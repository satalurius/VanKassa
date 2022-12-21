using Microsoft.EntityFrameworkCore;
using VanKassa.Backend.Core.Data.EmployeesSort;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Backend.Infrastructure.Data;
using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Core.Services;

public class EmployeesService : IEmployeesService
{
    private readonly VanKassaDbContext _dbContext;
    private readonly ImageService _imageService;
    private readonly SortEmployeesExecutor _sortEmployeesExecutor;

    public EmployeesService(VanKassaDbContext dbContext, ImageService imageService,
        SortEmployeesExecutor sortEmployeesExecutor)
    {
        _dbContext = dbContext;
        _imageService = imageService;
        _sortEmployeesExecutor = sortEmployeesExecutor;
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

            var employeesDto = await _dbContext.EmployeesDbDtos
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
        var deletedEmployees = _dbContext.Users.Where(emp => deletedIds.Contains(emp.UserId));
        _dbContext.Users.RemoveRange(deletedEmployees);

        var deletedUserOutlet = _dbContext.UserOutlets.Where(emp => deletedIds.Contains(emp.UserId));
        _dbContext.UserOutlets.RemoveRange(deletedUserOutlet);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<PageEmployeesDto?> GetEmployeesWithFiltersAsync(EmployeesPageParameters parameters)
    {
        try
        {
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
          """;

            var orderByStrategy = _sortEmployeesExecutor
                .GetSortImplementationByColumn(parameters.SortedColumn);

            var employeesDtoFromDb = await _dbContext.EmployeesDbDtos
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
                empDto.Photo = _imageService.CopyEmployeeImageToWebFolderAndGetCopyPath(empDto.Photo) ??
                               empDto.Photo;
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