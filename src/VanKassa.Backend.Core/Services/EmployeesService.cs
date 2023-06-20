using AutoMapper;
using Dapper;
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
    private readonly DapperDbContext dapperDbContext;
    private readonly IImageService imageService;
    private readonly SortEmployeesExecutor sortEmployeesExecutor;
    private readonly IMapper mapper;

    public EmployeesService(IDbContextFactory<VanKassaDbContext> dbContextFactory, IImageService imageService,
        SortEmployeesExecutor sortEmployeesExecutor, IMapper mapper, DapperDbContext dapperDbContext)
    {
        this.dbContextFactory = dbContextFactory;
        this.imageService = imageService;
        this.sortEmployeesExecutor = sortEmployeesExecutor;
        this.mapper = mapper;
        this.dapperDbContext = dapperDbContext;
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


    // TODO: Рефакторинг под Request сущность
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
            var query =
                """
              WITH grouped_cte as (SELECT dbo.employee.user_id, fist_name, last_name, patronymic, dbo.outlet.city, dbo.outlet.street,
                 dbo.outlet.street_number, r.name, dbo.employee.photo, dbo.employee.fired
                            FROM dbo.employee_outlet
                                     JOIN dbo.employee ON employee_outlet.user_id = employee.user_id
                                     JOIN dbo.outlet ON employee_outlet.outlet_id = outlet.outlet_id
                            JOIN dbo.role r ON employee.role_id = r.role_id)
              SELECT grouped_cte.user_id AS UserId,
                     string_agg(CONCAT(grouped_cte.city, ', ', grouped_cte.street, ', ', grouped_cte.street_number), '; ') Addresses,
                     name AS RoleName,
                     last_name AS LastName,
                     fist_name AS FirstName,
                     patronymic AS Patronymic,
                     photo AS Photo
                    
              FROM grouped_cte
              WHERE fired = false
              GROUP BY UserId, RoleName, LastName, FirstName, Patronymic, Photo
              ORDER BY UserId;
          """;

            using var dbConnection = dapperDbContext.CreateConnection();
            var employeesDbQuery = await dbConnection.QueryAsync<EmployeesDbDto>(query);
            var employeesDb = employeesDbQuery.ToList();
            
            if (!employeesDb.Any())
                throw new NotFoundException();

            var pageEmployees = new PageEmployeesDto
            {
                TotalCount = employeesDb.Count
            };

            var orderByStrategy = sortEmployeesExecutor
                .GetSortImplementationByColumn(parameters.SortedColumn);

            if (!string.IsNullOrEmpty(parameters.FilterText))
            {
                employeesDb = employeesDb
                    .Where(emp => emp.RoleName == parameters.FilterText ||
                                  emp.LastName.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.FirstName.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.Patronymic.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.Addresses.ToLower().Contains(parameters.FilterText.ToLower())).ToList();
            }

            employeesDb = orderByStrategy
                .SortEmployees(employeesDb, parameters.SortDirection)
                .Skip(parameters.Page * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

            employeesDb.ForEach(empDto => { empDto.Photo = imageService.ConvertImageToBase64(empDto.Photo); });

            pageEmployees.EmployeesDbDtos = employeesDb;

            return pageEmployees;
        }
        catch (ArgumentNullException ex)
        {
            // TODO: Подключить логгер
            throw new NotFoundException();
        }
    }
}