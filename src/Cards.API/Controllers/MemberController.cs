using Cards.API.Conventions;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Member;
using Cards.Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    [Authorize(Roles = "Member")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberCardService _cardService;
        private readonly IUserService _userService;

        public MemberController(IMemberCardService cardService, IUserService userService)
        {
            _cardService = cardService;
            _userService = userService;
        }

        [HttpPost()]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Create))]
        public async Task<IActionResult> AddCardAsync([FromBody] AddMemberCardRequest addCardRequest)
        {
            return new ObjectResult(await _cardService.AddCardAsync(addCardRequest));
        }

        [HttpPut("card/{id:guid}")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Update))]
        public async Task<IActionResult> UpdateCardAsync(Guid id, [FromBody] UpdateCardRequest updateCardRequest)
        {
            updateCardRequest.Id = id;
            return new ObjectResult(await _cardService.UpdateCardAsync(updateCardRequest));
        }

        [HttpDelete("card/{id:guid}")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteCardAsync(Guid id)
        {
            return new ObjectResult(await _cardService.DeleteCardAsync(id));
        }

        [HttpGet("card/{id:guid}")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetCardAsync(Guid id)
        {
            return new ObjectResult(await _cardService.GetCardAsync(id));
        }

        [HttpGet("cards")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetCardsAsync(GetCardsRequest getCardsRequest, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            return new ObjectResult(await _cardService.GetPaginatedCardsAsync(pageSize, pageIndex, getCardsRequest));
        }
    }
}
