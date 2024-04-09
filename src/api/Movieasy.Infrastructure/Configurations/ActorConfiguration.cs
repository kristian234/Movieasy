using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movieasy.Domain.Actors;

namespace Movieasy.Infrastructure.Configurations
{
    internal sealed class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(actor => actor.Id);

            builder.Property(actor => actor.Name)
                .HasMaxLength(ActorConstants.NameMaxLength)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.Property(actor => actor.Biography)
                .HasMaxLength(ActorConstants.BiographyMaxLength)
                .HasConversion(biography => biography.Value, value => new Biography(value));

            builder.HasIndex(actor => actor.Name)
                .IsUnique();
        }
    }
}
