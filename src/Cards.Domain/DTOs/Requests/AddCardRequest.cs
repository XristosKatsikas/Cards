using Swashbuckle.AspNetCore.Annotations;

namespace Cards.Domain.DTOs.Requests
{
    public record AddCardRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        [SwaggerSchema(ReadOnly = true)]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [SwaggerSchema(ReadOnly = true)]
        public string Status { get; set; } = Enums.Status.ToDo.ToString();
    }
}
