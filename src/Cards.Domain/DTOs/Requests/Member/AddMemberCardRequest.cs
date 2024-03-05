using Cards.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Cards.Domain.DTOs.Requests.Member
{
    public class AddMemberCardRequest : AddCardRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Role { get; set; } = Enums.Role.Member.ToString();
    }
}
