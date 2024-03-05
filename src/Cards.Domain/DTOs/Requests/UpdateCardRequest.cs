using Swashbuckle.AspNetCore.Annotations;

namespace Cards.Domain.DTOs.Requests
{
    public record UpdateCardRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
