using Microsoft.EntityFrameworkCore;
using Movieasy.Application.Abstractions.Data;
using Movieasy.Application.Abstractions.Messaging;
using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Actors;

namespace Movieasy.Application.Actors.GetActorById
{
    internal sealed class GetActorByIdQueryHandler : IQueryHandler<GetActorByIdQuery, ActorResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetActorByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ActorResponse>> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
        {
            ActorResponse? actor = await _context
                .Actors
                .Where(a => a.Id == request.ActorId)
                .Select(a => new ActorResponse()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name.Value,
                    Biography = a.Biography.Value,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if(actor == null)
            {
                return Result.Failure<ActorResponse>(ActorErrors.NotFound);
            }

            return actor;
        }
    }
}
