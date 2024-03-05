namespace Cards.Domain.DTOs.Requests
{
    public record GetCardRequest
    {
        public Guid Id { get; set; }
    }
}
