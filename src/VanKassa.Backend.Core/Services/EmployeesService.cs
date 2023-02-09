using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos.Employees;

namespace VanKassa.Backend.Core.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IDbContextFactory<VanKassaDbContext> _dbContextFactory;
    private readonly ImageService _imageService;
    private readonly SortEmployeesExecutor _sortEmployeesExecutor;
    private readonly IMapper _mapper;

    public EmployeesService(IDbContextFactory<VanKassaDbContext> dbContextFactory, ImageService imageService,
        SortEmployeesExecutor sortEmployeesExecutor, IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _imageService = imageService;
        _sortEmployeesExecutor = sortEmployeesExecutor;
        _mapper = mapper;
    }

    /// <summary>
    /// Возвращает список сотрудников для вывода на интерфейс.
    /// И путь к фотографии скопированной в каталог wwwroot/images/employees
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<EmployeesDbDto>?> GetEmployeesAsync()
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var query =
                """
              WITH grouped_cte as (SELECT "user"."UserId", fist_name, last_name, patronymic, "outlet".city, "outlet".street, "outlet".street_number, r.name, "user".photo
                            FROM user_outlet
                                     JOIN "user" ON user_outlet."UserId" = "user"."UserId"
                                     JOIN "outlet" ON user_outlet."OutletId" = outlet."OutletId"
                            JOIN role r ON "user"."RoleId" = r."RoleId")
              SELECT grouped_cte."UserId" AS UserId,
                     string_agg(CONCAT(grouped_cte.city, ', ', grouped_cte.street, ', ', grouped_cte.street_number), '; ') Addresses,
                     name AS RoleName,
                     last_name AS LastName,
                     fist_name AS FirstName,
                     patronymic AS Patronymic,
                     photo AS Photo
              FROM grouped_cte
              GROUP BY UserId, RoleName, LastName, FirstName, Patronymic, Photo
              ORDER BY UserId;
          """;

            var employeesDto = await dbContext.EmployeesDbDtos
                .FromSqlRaw(query)
                .ToListAsync();

            employeesDto.ForEach(empDto =>
            {
                empDto.Photo = _imageService.CopyEmployeeImageToWebFolderAndGetCopyPath(empDto.Photo) ??
                               empDto.Photo;
            });

            return employeesDto;
        }
        catch (ArgumentNullException ex)
        {
            // TODO: Подключить логгер
            return null;
        }
    }

    public async Task DeleteEmployeesAsync(IEnumerable<int> deletedIds)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        
        var deletedUserOutlet = await dbContext.EmployeeOutlets.Where(emp => deletedIds.Contains(emp.UserId))
            .ToListAsync();
        dbContext.EmployeeOutlets.RemoveRange(deletedUserOutlet);
        
        await dbContext.SaveChangesAsync();

        var deletedEmployees = await dbContext.Employees.Where(emp => deletedIds.Contains(emp.UserId))
            .ToListAsync();

        deletedEmployees.ForEach(emp => emp.Fired = true);
        
        await dbContext.SaveChangesAsync();

        await transaction.CommitAsync();
    }

    public async Task<PageEmployeesDto?> GetEmployeesWithFiltersAsync(EmployeesPageParameters parameters)
    {
        try
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var query =
                """
              WITH grouped_cte as (SELECT "user"."UserId", fist_name, last_name, patronymic, "outlet".city, "outlet".street, "outlet".street_number, r.name, "user".photo, "user".fired as fired
                            FROM user_outlet
                                     JOIN "user" ON user_outlet."UserId" = "user"."UserId"
                                     JOIN "outlet" ON user_outlet."OutletId" = outlet."OutletId"
                            JOIN role r ON "user"."RoleId" = r."RoleId")
              SELECT grouped_cte."UserId" AS UserId,
                     string_agg(CONCAT(grouped_cte.city, ', ', grouped_cte.street, ', ', grouped_cte.street_number), '; ') Addresses,
                     name AS RoleName,
                     last_name AS LastName,
                     fist_name AS FirstName,
                     patronymic AS Patronymic,
                     photo AS Photo
              FROM grouped_cte
              WHERE fired IS NOT NULL 
              GROUP BY UserId, RoleName, LastName, FirstName, Patronymic, Photo
          """;

            var orderByStrategy = _sortEmployeesExecutor
                .GetSortImplementationByColumn(parameters.SortedColumn);

            var employeesDtoFromDb = await dbContext.EmployeesDbDtos
                .FromSqlRaw(query)
                .ToListAsync();

            var pageEmployees = new PageEmployeesDto()
            {
                TotalCount = employeesDtoFromDb.Count
            };

            if (!string.IsNullOrEmpty(parameters.FilterText))
            {
                employeesDtoFromDb = employeesDtoFromDb
                    .Where(emp => emp.RoleName == parameters.FilterText ||
                                  emp.LastName.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.FirstName.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.Patronymic.ToLower().Contains(parameters.FilterText.ToLower()) ||
                                  emp.Addresses.ToLower().Contains(parameters.FilterText.ToLower())).ToList();
                
                employeesDtoFromDb = orderByStrategy
                    .SortEmployees(employeesDtoFromDb, parameters.SortDirection)
                    .Skip(parameters.Page * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToList();
            }
            else
            {
                employeesDtoFromDb = orderByStrategy.SortEmployees(employeesDtoFromDb, parameters.SortDirection)
                    .Skip(parameters.Page * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToList();
            }

            employeesDtoFromDb.ForEach(empDto =>
            {
                empDto.Photo = _imageService.ConvertImageToBase64(empDto.Photo);
            });

            pageEmployees.EmployeesDbDtos = employeesDtoFromDb;
            
            return pageEmployees;
        }
        catch (ArgumentNullException ex)
        {
            // TODO: Подключить логгер
            return null;
        }
    }
}