using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class CardUpdateValidator : AbstractValidator<CardUpdateRequest>
    {
        public CardUpdateValidator()
        {
            RuleFor(c => c.CardId)
                .NotNull()
                .NotEmpty()
                .Length(36);

            RuleFor(x => x.ClientId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.CardNo)
                .NotNull()
                .NotEmpty()
                .Length(16);

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.CVV2)
                .NotNull()
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.ExpDate)
                .NotNull()
                .NotEmpty();
        }
    }
}
