namespace Movieasy.Domain.Users
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(User user);
    }
}
