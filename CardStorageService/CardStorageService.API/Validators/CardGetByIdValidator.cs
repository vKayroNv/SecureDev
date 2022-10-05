using CardStorageService.API.Models.Requests;
using FluentValidation;

namespace CardStorageService.API.Validators
{
    public class CardGetByIdValidator : AbstractValidator<CardGetByIdRequest>
    {
        public CardGetByIdValidator()
        {
            RuleFor(c => c.CardId)
                .NotNull()
                .NotEmpty()
                .Length(36);
        }
    }
}
