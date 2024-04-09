using Movieasy.Domain.Actors;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Remove(Actor actor)
        {
            DbContext.Actors.Remove(actor);
        }
    }
}
