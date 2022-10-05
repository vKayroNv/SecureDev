using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class AuthRegisterValidator : AbstractValidator<AuthRegisterRequest>
    {
        public AuthRegisterValidator()
        {
            RuleFor(x => x.EMail)
                .NotNull()
                .NotEmpty()
                .Length(5, 255)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Length(8, 255);

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Surname)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.Patronymic)
                .MaximumLength(255);
        }
    }
}