using FluentValidation;
using VanKassa.Domain.ViewModels;

namespace VanKassa.Presentation.BlazorWeb.Shared.Data.Validation
{
    public class AuthorizationValidations : AbstractValidator<AuthorizationViewModel>
    {
        public AuthorizationValidations()
        {
            RuleFor(p => p.Login)
                .NotEmpty()
                .MinimumLength(1)
                .WithMessage("Логин введен неверно");

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotEmpty()
                .MinimumLength(1)
                .WithMessage("Пароль введен неверно");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<AuthorizationViewModel>
                .CreateWithOptions((AuthorizationViewModel)model,
                    x => x.IncludeProperties(propertyName)));
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}