using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.Enums;

public enum EmployeeTableColumn
{
    [Display(Name = "Пустая колонка")]
    None = 0,
    [Display(Name = "Фотография")]
    Photo = 1,
    [Display(Name = "Фамилия")]
    LastName = 2,
    [Display(Name = "Имя")]
    FirstName = 3,
    [Display(Name = "Отчество")]
    Patronymic = 4,
    [Display(Name = "Торговые точки")]
    OutletAddresses = 5,
    [Display(Name = "Должность")]
    Role = 6,
    [Display(Name = "ФИО")]
    FullName = 7,
}