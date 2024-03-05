using Cards.Domain.Configurations;
using Cards.Domain.DTOs.Requests.User;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Entities;
using Cards.Domain.Repositories.Abstractions;
using Cards.Domain.Services.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cards.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _userRepository = userRepository;
            _jwtTokenSettings = jwtTokenSettings.Value;
        }

        public async Task<UserResponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            return new UserResponse { Email = response.Email! };
        }

        public async Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken cancellationToken)
        {
            bool isAuthenticated = await _userRepository.AuthenticateAsync(request.Email, request.Password, cancellationToken);

            if (!isAuthenticated)
            {
                return null!;
            }

            var user = new ApplicationUser { Email = request.Email, UserName = request.Email, Password = request.Password};

            return new TokenResponse { Token = GenerateSecurityToken(user) };
        }

        /// <summary>
        /// Return a new UserResponse instance which determines the information related to the signed user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { Email = request.Email, UserName = request.Email };
            bool isCreated = await _userRepository.SignUpAsync(user, request.Password, cancellationToken);

            if (!isCreated)
            {
                return null!;
            }
            return new UserResponse { UserName = request.Username!, Email = request.Email };
        }

        private string GenerateSecurityToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var expiration = DateTime.UtcNow.AddDays(_jwtTokenSettings.ExpirationDays);
            var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration);

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
        new(
            _jwtTokenSettings.ValidIssuer,
            _jwtTokenSettings.ValidAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

        private List<Claim> CreateClaims(ApplicationUser user)
        {
            var jwtSub = _jwtTokenSettings.JwtRegisteredClaimNamesSub;

            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            var symmetricSecurityKey = Encoding.ASCII.GetBytes(_jwtTokenSettings.Secret);
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(symmetricSecurityKey.ToString()!)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
