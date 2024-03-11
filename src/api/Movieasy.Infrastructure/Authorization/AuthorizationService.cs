using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Domain.Users;

namespace Movieasy.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly IApplicationDbContext _context;

        public AuthorizationService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            var roles = await _context.Users
                .Where(user => user.IdentityId == identityId)
                .Select(user => new UserRolesResponse()
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();

            return roles;
        }
    }
}
