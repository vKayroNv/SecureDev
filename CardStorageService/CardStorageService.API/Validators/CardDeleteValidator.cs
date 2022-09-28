using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class CardDeleteValidator : AbstractValidator<CardDeleteRequest>
    {
        public CardDeleteValidator()
        {
            RuleFor(c => c.CardId)
                .NotNull()
                .NotEmpty()
                .Length(36);
        }
    }
}
