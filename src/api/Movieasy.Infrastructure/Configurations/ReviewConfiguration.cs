using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieasy.Domain.Movies;
using Movieasy.Domain.Reviews;
using Movieasy.Domain.Users;
using Rating = Movieasy.Domain.Reviews.Rating;

namespace Movieasy.Infrastructure.Configurations
{
    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(review => review.Id);

            builder.Property(review => review.Comment)
                .HasMaxLength(1000)
                .HasConversion(comment => comment.Value, value => new Comment(value));

            builder.Property(review => review.Rating)
                .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

            builder.HasOne<Movie>()
                .WithMany()
                .HasForeignKey(review => review.MovieId);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(review => review.UserId);
        }
    }
}
