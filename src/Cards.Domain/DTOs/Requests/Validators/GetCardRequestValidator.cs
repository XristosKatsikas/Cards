using Cards.Domain.Enums;
using FluentValidation;

namespace Cards.Domain.DTOs.Requests.Validators
{
    public class GetCardRequestValidator : AbstractValidator<GetCardRequest>
    {
        public GetCardRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Card Id should not be empty");
        }
    }
}
