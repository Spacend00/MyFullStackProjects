
using Microsoft.Extensions.DependencyInjection;
using PostAppAPI.Application.Interfaces;
using PostAppAPI.Infrastructure.Repositories;
using PostAppAPI.Infrastructure.Services;

namespace PostAppAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
