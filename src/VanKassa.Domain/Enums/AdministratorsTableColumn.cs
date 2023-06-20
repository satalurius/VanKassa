using System.ComponentModel.DataAnnotations;

namespace VanKassa.Domain.Enums
{
    public enum AdministratorsTableColumn
    {
        [Display(Name = "ФИО")]
        FullName = 0,
        [Display(Name = "Номер телефона")]
        Phone = 1,
        [Display(Name = "Имя пользователя")]
        UserName = 2,
        [Display(Name = "Пароль")]
        Password = 3,
    }
}