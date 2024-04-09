using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieasy.Domain.Genres;
using Movieasy.Domain.Movies;

namespace Movieasy.Infrastructure.Configurations
{
    internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(genre => genre.Id);

            builder.Property(genre => genre.Name)
                .HasMaxLength(GenreConstants.NameMaxLength)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.HasIndex(genre => genre.Name)
                .IsUnique();

            builder.HasMany<Movie>()
                .WithMany(movie => movie.Genres);
        }
    }
}
