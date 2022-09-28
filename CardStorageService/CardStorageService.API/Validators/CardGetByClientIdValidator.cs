using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class CardGetByClientIdValidator : AbstractValidator<CardGetByClientIdRequest>
    {
        public CardGetByClientIdValidator()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
