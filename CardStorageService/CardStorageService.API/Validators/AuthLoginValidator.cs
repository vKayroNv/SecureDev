using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class AuthLoginValidator : AbstractValidator<AuthLoginRequest>
    {
        public AuthLoginValidator()
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
        }
    }
}