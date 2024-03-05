using Cards.Domain.Entities;
using Cards.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cards.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AuthenticateAsync(string email, string password, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(
                email, password, false, false);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string requestEmail, CancellationToken cancellationToken)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.Email == requestEmail, cancellationToken);
        }

        public async Task<bool> SignUpAsync(ApplicationUser user, string password, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }
    }
}
