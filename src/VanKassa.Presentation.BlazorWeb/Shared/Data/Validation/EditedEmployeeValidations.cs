using FluentValidation;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data.Validation;

public class EditedEmployeeValidation : AbstractValidator<EditedEmployeeViewModel>
{
    public EditedEmployeeValidation()
    {
        RuleFor(p => p.LastName)
            .NotEmpty()
            .MinimumLength(1)
            .WithMessage("Не указана фамилия");
        
        RuleFor(p => p.FirstName)
            .NotEmpty()
            .MinimumLength(1)
            .WithMessage("Не указано имя");
        
        
        RuleFor(p => p.Patronymic)
            .NotEmpty()
            .MinimumLength(1)
            .WithMessage("Не указано отчество");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EditedEmployeeViewModel>
            .CreateWithOptions((EditedEmployeeViewModel)model,
                x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}

public class EmployeeRoleValidation : AbstractValidator<EmployeeRoleViewModel>
{
    public EmployeeRoleValidation()
    {
        RuleFor(p => p.RoleName)
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Роль не выбрана");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EmployeeRoleViewModel>
            .CreateWithOptions((EmployeeRoleViewModel)model,
                x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}

public class EmployeeOutletValidation : AbstractValidator<EmployeeOutletViewModel>
{
    public EmployeeOutletValidation()
    {
        RuleFor(p => p.Address)
            .NotEmpty()
            .MinimumLength(2)
            .WithMessage("Роль не выбрана");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<EmployeeOutletViewModel>
            .CreateWithOptions((EmployeeOutletViewModel)model,
                x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}