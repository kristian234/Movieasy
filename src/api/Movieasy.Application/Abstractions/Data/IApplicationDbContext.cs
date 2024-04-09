using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Actors;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Photos;
using Movieasy.Domain.Reviews;
using Movieasy.Domain.Users;

namespace Movieasy.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
