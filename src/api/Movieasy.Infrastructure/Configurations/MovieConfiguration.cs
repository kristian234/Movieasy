﻿using Microsoft.EntityFrameworkCore;
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
                .HasMaxLength(MovieConstants.TitleMaxLength)
                .HasConversion(title => title.Value, value => new Title(value));

            builder.Property(movie => movie.Description)
                .HasMaxLength(MovieConstants.DescriptionMaxLength)
                .HasConversion(description => description.Value, value => new Description(value));

            builder.Property(movie => movie.Duration)
                .HasConversion(duration => duration.Value, value => Duration.Create(value).Value);

            builder.Property(movie => movie.Trailer)
                .HasMaxLength(MovieConstants.TrailerMaxLength)
                .HasConversion(trailer => trailer.Value, value => new Trailer(value));

            builder.HasOne(movie => movie.Photo)
                .WithOne()
                .HasForeignKey<Movie>(movie => movie.PhotoId);
        }
    }
}
