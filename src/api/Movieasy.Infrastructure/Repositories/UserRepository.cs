using Movieasy.Domain.Users;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task AddAsync(User entity)
        {
            foreach(var role in entity.Roles)
            {
                DbContext.Attach(role);
            }

           await DbContext.AddAsync(entity);
        }

    }
}
