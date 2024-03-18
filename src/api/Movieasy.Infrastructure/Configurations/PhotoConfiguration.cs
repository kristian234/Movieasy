using Microsoft.EntityFrameworkCore;
using Movieasy.Domain.Photos;

namespace Movieasy.Infrastructure.Configurations
{
    internal sealed class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("photos");

            builder.HasKey(photo => photo.Id);

            builder.Property(photo => photo.PublicId)
                .HasConversion(publicId => publicId.Value, value => new PublicId(value));

            builder.Property(photo => photo.Url)
                .HasConversion(url => url.Value, value => new Url(value));
        }
    }
}
