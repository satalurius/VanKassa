using VanKassa.Domain.Dtos.Employees;
using VanKassa.Domain.Entities;

namespace VanKassa.Backend.Core.Tests;

public static class DataCreators
{
    public static IEnumerable<Outlet> CreateFakeOutletsList()
    {
        var outlets = new List<Outlet>();
        const int size = 3;
        for (var i = 1; i < size; i++)
        {
            outlets.Add(
                new Outlet
                {
                    OutletId = i,
                    City = "City",
                    Street = "Street",
                    StreetNumber = $"{i * 24}" 
                });
        }

        return outlets;
    }
    public static IEnumerable<Employee> CreateFakeDbEmployeesList(int size)
    {
        var dbEmployees = new List<Employee>();

        for (var i = 1; i < size; i++)
        {
            Employee createdEmployee;
            if (i < size / 2)
            {
                createdEmployee = new Employee
                {
                    UserId = i,
                    FirstName = $"Сотрудник {i}",
                    LastName = "LastName",
                    Patronymic = "Patronymic",
                    Photo = "photo_path",
                    Role = new Role
                    {
                        RoleId = i,
                        Name = "Role"
                    },
                    Fired = false,
                    UserOutlets = new List<EmployeeOutlet>
                    {
                        new()
                        {
                            UserId = i,
                            OutletId = 1
                        }
                    },
                };
            }
            else
            {
                createdEmployee = new Employee
                {
                    UserId = i,
                    FirstName = "Какое-то имя",
                    LastName = $"Фамилия {i}",
                    Patronymic = "Patronymic",
                    Photo = "photo_path",
                    Role = new Role
                    {
                        RoleId = i,
                        Name = "Role"
                    },
                    Fired = false,
                    UserOutlets = new List<EmployeeOutlet>
                    {
                        new()
                        {
                            UserId = i,
                            OutletId = 2
                        }
                    }
                };
            }

            dbEmployees.Add(createdEmployee);
        }

        return dbEmployees;
    }

    public static IEnumerable<EmployeesDbDto> CreateFakeEmployeesDtoList(int size)
    {
        var employeesDbDtos = new List<EmployeesDbDto>();

        for (var i = 1; i < size; i++)
        {
            EmployeesDbDto addedEmp;
            if (i < size / 2)
            {
                addedEmp = new EmployeesDbDto
                {
                    UserId = i,
                    Addresses = "addresses",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Patronymic = "Patronymic",
                    Photo = "photo_path",
                    RoleName = "Roe"
                };
            }
            else
            {
                addedEmp = new EmployeesDbDto
                {
                    UserId = i,
                    Addresses = "addresses",
                    FirstName = "NoFirstName",
                    LastName = "Surname",
                    Patronymic = "Patronymic",
                    Photo = "photo_path",
                    RoleName = "Roe"
                };
            }

            employeesDbDtos.Add(addedEmp);
        }

        return employeesDbDtos;
    }
}