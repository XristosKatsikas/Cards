using Cards.Domain.Enums;

namespace Cards.Domain.DTOs.Responses
{
    public record CardResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public Status Status { get; set; }

        public DateTime DateCreated { get; set; }
        public string UserRole { get; set; } = string.Empty;
    }
}
