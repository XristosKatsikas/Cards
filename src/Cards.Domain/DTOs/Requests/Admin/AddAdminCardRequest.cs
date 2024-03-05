using Cards.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Cards.Domain.DTOs.Requests.Admin
{
    public record AddAdminCardRequest : AddCardRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string UserRole { get; set; } = Role.Admin.ToString();
    }
}
