using Cards.Domain.Services;
using Cards.Domain.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Domain.Extensions
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICardService, CardService>()
                .AddScoped<IMemberCardService, MemberCardService>()
                .AddScoped<IAdminCardService, AdminCardService>()
                .Decorate<IAdminCardService, AdminCardServiceDecorator>();

            return services;
        }
    }
}