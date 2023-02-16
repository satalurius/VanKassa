using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Exceptions;

namespace VanKassa.Backend.Core.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IDbContextFactory<VanKassaDbContext> dbContextFactory;
    private readonly IImageService imageService;
    private readonly SortEmployeesExecutor sortEmployeesExecutor;
    private readonly IMapper mapper;

    public EmployeesService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IImageService imageService,
        SortEmployeesExecutor sortEmployeesExecutor, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.imageService = imageService;
        this.sortEmployeesExecutor = sortEmployeesExecutor;
        this.mapper = mapper;
    }

    /// <summary>
    /// Возвращает список сотрудников для вывода на интерфейс.
    /// И путь к фотографии скопированной в каталог wwwroot/images/employees
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<EmployeesDbDto>> GetEmployeesAsync()
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var employeesDb = await dbContext.Employees
                .Include(i => i.UserOutlets)
                .ThenInclude(i => i.Outlet)
                .Include(i => i.Role)
                .ToListAsync();

            if (!employeesDb.Any())
                throw new NotFoundException();

            employeesDb.ForEach(empDto =>
            {
                empDto.Photo = imageService.CopyEmployeeImageToWebFolderAndGetCopyPath(empDto.Photo) ??
                               empDto.Photo;
            });

            var employeesDto = mapper.Map<List<EmployeesDbDto>>(employeesDb);

            return employeesDto;
        }
        catch (ArgumentNullException ex)
        {
            // TODO: Подключить логгер
            throw new NotFoundException();
        }
    }

    public async Task DeleteEmployeesAsync(IEnumerable<int> deletedIds)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();
            await using var transaction = await dbContext.Database.BeginTransactionAsync();

            var deletedUserOutlet = await dbContext.EmployeeOutlets.Where(emp => deletedIds.Contains(emp.UserId))
                .ToListAsync();
            dbContext.EmployeeOutlets.RemoveRange(deletedUserOutlet);

            await dbContext.SaveChangesAsync();

            var deletedEmployees = await dbContext.Employees.Where(emp => deletedIds.Contains(emp.UserId))
                .ToListAsync();

            if (!deletedEmployees.Any())
                throw new BadRequestException("Delete was failed");

            deletedEmployees.ForEach(emp => emp.Fired = true);

            await dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (OperationCanceledException)
        {
            throw new BadRequestException("Delete was failed");
        }
        catch (ArgumentNullException)
        {
            throw new BadRequestException("Delete was failed");
        }
    }

    public async Task<PageEmployeesDto> GetEmployeesWithFiltersAsync(EmployeesPageParameters parameters)
    {
        try
        {
            await using var dbContext = await dbContextFactory.CreateDbContextAsync();

            var employeesDb = await dbContext.Employees
                .Include(i => i.UserOutlets)
                .ThenInclude(i => i.Outlet)
                .Include(i => i.Role)
                .Where(x => !x.Fired)
                .ToListAsync();

            if (!employeesDb.Any())
                throw new NotFoundException();

            var pageEmployees = new PageEmployeesDto
            {
                TotalCount = employeesDb.Count
            };

            var orderByStrategy = sortEmployeesExecutor
                .GetSortImplementationByColumn(parameters.SortedColumn);

            var employeesDto = mapper.Map<List<EmployeesDbDto>>(employeesDb);

            if (!string.IsNullOrEmpty(parameters.FilterText))
            {
                employeesDto = employeesDto
                    .Where(emp => emp.RoleName == parameters.FilterText ||
                                  emp.LastName.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.FirstName.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.Patronymic.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.Addresses.ToLower().Contains(parameters.FilterText.ToLower())).ToList();
            }

            employeesDto = orderByStrategy
                .SortEmployees(employeesDto, parameters.SortDirection)
                .Skip(parameters.Page * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

            employeesDto.ForEach(empDto => { empDto.Photo = imageService.ConvertImageToBase64(empDto.Photo); });

            pageEmployees.EmployeesDbDtos = employeesDto;

            return pageEmployees;
        }
        catch (ArgumentNullException ex)
        {
            // TODO: Подключить логгер
            throw new NotFoundException();
        }
    }
}