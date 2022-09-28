using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class ClientDeleteValidator : AbstractValidator<ClientDeleteRequest>
    {
        public ClientDeleteValidator()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
