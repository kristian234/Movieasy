using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Profiles.GetProfileById
{
    internal sealed class GetProfileByIdQueryHandler : IQueryHandler<GetProfileByIdQuery, ProfileResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetProfileByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ProfileResponse>> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            ProfileResponse? profile = await _context.
                Users
                .Include(u => u.Reviews)
                .Where(u => u.Id == request.UserId)
                .Select(u => new ProfileResponse()
                {
                    Id = u.Id.ToString(),
                    FirstName = u.FirstName.Value,
                    LastName = u.LastName.Value,
                    Details = u.Details.Value
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                return Result.Failure<ProfileResponse>(UserErrors.NotFound);
            }

            return profile;
        }
    }
}
