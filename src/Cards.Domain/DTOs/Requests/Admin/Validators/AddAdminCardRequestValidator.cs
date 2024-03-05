using Cards.Domain.Enums;
using Cards.Domain.Extensions;
using FluentValidation;

namespace Cards.Domain.DTOs.Requests.Admin.Validators
{
    public class AddAdminCardRequestValidator : AbstractValidator<AddAdminCardRequest>
    {
        public AddAdminCardRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name must not be empty");
            RuleFor(x => x.Description)
                .Must(x => x.Length <= 100);
            RuleFor(x => x.Color)
                .Must(x => x.StartsWith("#") && x.Length == 6)
                .WithMessage("Color must start with # and have a length of exactly 6 chars");
            RuleFor(x => x.Color.Matches("^[0-9a-zA-Z ]+#"));
            RuleFor(x => x.Status)
                .Must(x => x.Equals(Status.ToDo));
            RuleFor(x => x.DateCreated)
                .Must(x => x == DateTime.UtcNow);
            RuleFor(x => x.UserRole)
                .NotEmpty()
                .Must(x => x.Equals(Role.Admin.ToString()));
        }
    }
}
