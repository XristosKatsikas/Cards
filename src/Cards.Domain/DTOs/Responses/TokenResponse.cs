namespace Cards.Domain.DTOs.Responses
{
    public record TokenResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
