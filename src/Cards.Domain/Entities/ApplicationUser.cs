using Cards.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Cards.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Role Role { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
