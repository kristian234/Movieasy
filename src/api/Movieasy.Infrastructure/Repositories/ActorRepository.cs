using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Actor>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await DbContext.Actors
                .Where(actor => ids.Contains(actor.Id))
                .ToListAsync();
        }


        public void Remove(Actor actor)
        {
            DbContext.Actors.Remove(actor);
        }
    }
}
