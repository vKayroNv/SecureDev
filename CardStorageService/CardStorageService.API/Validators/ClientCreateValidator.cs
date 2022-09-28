using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class ClientCreateValidator : AbstractValidator<ClientCreateRequest>
    {
        public ClientCreateValidator()
        {
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
