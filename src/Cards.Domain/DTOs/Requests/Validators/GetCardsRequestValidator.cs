using Cards.Domain.Enums;
using Cards.Domain.Extensions;
using FluentValidation;

namespace Cards.Domain.DTOs.Requests.Validators
{
    public class GetCardsRequestValidator : AbstractValidator<GetCardsRequest>
    {
        public GetCardsRequestValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .Must(x => x.Equals(Status.ToDo) || x.Equals(Status.InProgress) || x.Equals(Status.Done));
            RuleFor(x => x.DateCreated)
                .NotEmpty()
                .Must(x => x <= DateTime.UtcNow)
                .WithMessage("DateCreated must not be less than UtcNow"); ;
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name must not be ampty");
            RuleFor(x => x.Color)
                .NotEmpty()
                .WithMessage("Color must not be empty");
            RuleFor(x => x.Color)
                .Must(x => x.StartsWith("#") && x.Length == 6)
                .WithMessage("Color must start with # and have a length of exactly 6 chars");
            RuleFor(x => x.Color.Matches("^[0-9a-zA-Z ]+#"));
        }
    }
}
