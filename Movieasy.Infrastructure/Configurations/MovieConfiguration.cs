using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieasy.Domain.Movies;

namespace Movieasy.Infrastructure.Configurations
{
    internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("movies");

            builder.HasKey(movie => movie.Id);

            builder.Property(movie => movie.Title)
                .HasMaxLength(200)
                .HasConversion(title => title.Value, value => new Title(value));

            builder.Property(movie => movie.Description)
                .HasMaxLength(1000)
                .HasConversion(description => description.Value, value => new Description(value));

            builder.Property(movie => movie.Duration)
                .HasConversion(duration => duration.Value, value => new Duration(value));
        }
    }
}
