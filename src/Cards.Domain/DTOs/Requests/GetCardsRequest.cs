namespace Cards.Domain.DTOs.Requests
{
    public record GetCardsRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }
    }
}
