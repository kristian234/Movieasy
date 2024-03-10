using Microsoft.AspNetCore.Http;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Infrastructure.Authentication.Models;

namespace Movieasy.Infrastructure.Authentication
{
    internal sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId =>
            _httpContextAccessor
                .HttpContext?
                .User
                .GetUserId() ??
            throw new ApplicationException("User Context is unavailable");

        public string IdentityId =>
            _httpContextAccessor
                .HttpContext?
                .User
                .GetIdentityId() ??
            throw new ApplicationException("User Context is unavailable");
    }
}
