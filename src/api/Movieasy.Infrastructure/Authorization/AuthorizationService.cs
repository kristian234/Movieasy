using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Caching;
using Movieasy.Application.Abstractions.Data;

namespace Movieasy.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public AuthorizationService(
            IApplicationDbContext context,
            ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            var cacheKey = $"auth:roles-{identityId}";

            var cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

            if (cachedRoles is not null)
            {
                return cachedRoles;                
            }

            var roles = await _context.Users
                .Where(user => user.IdentityId == identityId)
                .Select(user => new UserRolesResponse()
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();

            await _cacheService.SetAsync(cacheKey, roles);

            return roles;
        }
    }
}
