namespace Cards.Domain.DTOs.Responses
{
    public record UserResponse
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
