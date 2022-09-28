using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class ClientGetByIdValidator : AbstractValidator<ClientGetByIdRequest>
    {
        public ClientGetByIdValidator()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
