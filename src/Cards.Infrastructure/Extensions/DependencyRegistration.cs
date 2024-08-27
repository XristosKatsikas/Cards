using Cards.Domain.Repositories.Abstractions;
using Cards.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure.Extensions
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<ICardRepository, CardRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
