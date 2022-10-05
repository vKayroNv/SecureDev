using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class ClientUpdateValidator : AbstractValidator<ClientUpdateRequest>
    {
        public ClientUpdateValidator()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

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
