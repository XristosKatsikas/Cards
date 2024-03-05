using Cards.API.Conventions;
using Cards.Core;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("/[controller]/")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminCardService _cardService;
        private readonly IUserService _userService;

        public AdminController(IAdminCardService cardService, IUserService userService)
        {
            _cardService = cardService;
            _userService = userService;
        }

        [HttpPost()]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Create))]
        public async Task<IActionResult> AddCardAsync(AddAdminCardRequest addCardRequest)
        {
            return this.ApiResponse(await _cardService.AddCardAsync(addCardRequest));
        }

        [HttpPut("card/{id:guid}")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Update))]
        public async Task<IActionResult> UpdateCardAsync(Guid id, UpdateCardRequest updateCardRequest)
        {
            updateCardRequest.Id = id;
            return this.ApiResponse(await _cardService.UpdateCardAsync(updateCardRequest));
        }

        [HttpDelete("card/{id:guid}")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> DeleteCardAsync(Guid id, DeleteCardRequest deleteCardRequest)
        {
            deleteCardRequest.Id = id;
            return this.ApiResponse(await _cardService.DeleteCardAsync(deleteCardRequest));
        }

        [HttpGet("card/{id:guid}")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetCardAsync(Guid id, GetCardRequest getCardRequest)
        {
            getCardRequest.Id = id;
            return this.ApiResponse(await _cardService.GetCardAsync(getCardRequest));
        }

        [HttpGet("cards")]
        [ApiConventionMethod(typeof(ApiConvention), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetCardsAsync(GetCardsRequest getCardsRequest, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            return this.ApiResponse(await _cardService.GetPaginatedCardsAsync(pageSize, pageIndex, getCardsRequest));
        }
    }
}
