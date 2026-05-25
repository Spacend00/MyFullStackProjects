using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccesor = httpContextAccessor;
        }
        public string? UserId => _httpContextAccesor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
