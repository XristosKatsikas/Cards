using Cards.Domain.Enums;

namespace Cards.Domain.DTOs.Requests.User
{
    public record SignUpRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
