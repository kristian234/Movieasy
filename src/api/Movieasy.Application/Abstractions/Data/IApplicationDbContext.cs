using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using Movieasy.Domain.Reviews;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
