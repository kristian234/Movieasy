namespace Movieasy.Domain.Actors
{
    public interface IActorRepository
    {
        public Task<Actor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        public Task AddAsync(Actor actor);
        public void Remove(Actor actor);
    }
}
