using FluentValidation;

namespace Cards.Domain.DTOs.Requests.Validators
{
    public class DeleteCardRequestValidator : AbstractValidator<DeleteCardRequest>
    {
        public DeleteCardRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Card Id should not be empty");
        }
    }
}
