using Cards.Domain.DTOs.Requests.User;
using Cards.Domain.DTOs.Responses;

namespace Cards.Domain.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserResponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken = default);
        Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken cancellationToken = default);
        Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken = default);
    }
}
