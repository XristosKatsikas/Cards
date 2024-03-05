using Cards.Domain.Entities;

namespace Cards.Domain.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<ApplicationUser> GetByEmailAsync(string requestEmail, CancellationToken cancellationToken = default);
        Task<bool> SignUpAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default);
    }
}
