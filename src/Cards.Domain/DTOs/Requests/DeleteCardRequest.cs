namespace Cards.Domain.DTOs.Requests
{
    public record DeleteCardRequest
    {
        public Guid Id { get; set; }
    }
}
