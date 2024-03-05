using Cards.Domain.DTOs.Requests.User;
using Cards.Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("/[controller]/")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMemberCardService _cardService;
        private readonly IUserService _userService;

        public UserController(IMemberCardService cardService, IUserService userService)
        {
            _cardService = cardService;
            _userService = userService;
        }

        [HttpGet("member")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            if (claim == null)
            {
                return Unauthorized();
            }

            var token = await _userService.GetUserAsync(new GetUserRequest { Email = claim.Value });
            return Ok(token);
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            if (claim == null)
            {
                return Unauthorized();
            }

            var token = await _userService.SignInAsync(request);

            if (token == null)
            {
                return BadRequest();
            }
            return Ok(token);
        }

        /// <summary>
        /// Registers a new user and returns the 201 Created HTTP code if the operation has success
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            var user = await _userService.SignUpAsync(request);

            if (user == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { }, null);
        }
    }
}