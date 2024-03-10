using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Authentication;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Users.GetLoggedInUser
{
    internal sealed class GetLoggedInUserQueryHandler
        : IQueryHandler<GetLoggedInUserQuery, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public GetLoggedInUserQueryHandler(
            IApplicationDbContext context,
            IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<UserResponse>> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            UserResponse? user = await _context.Users
                .Where(x => x.IdentityId == _userContext.IdentityId)
                .Select(x => new UserResponse()
                {
                    Id = x.Id,
                    Email = x.Email.Value,
                    FirstName = x.FirstName.Value,
                    LastName = x.LastName.Value
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                return Result.Failure<UserResponse>(UserErrors.InvalidCredentials);
            }

            return user;
        }
    }
}
