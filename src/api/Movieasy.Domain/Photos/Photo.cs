using Movieasy.Domain.Abstractions;
using Movieasy.Domain.Movies;

namespace Movieasy.Domain.Photos
{
    public sealed class Photo : Entity
    {
        private Photo(
            Guid id,
            PublicId publicId,
            Url url) : base(id)
        {
            PublicId = publicId;
            Url = url;
        }

        public PublicId PublicId { get; private set; }
        public Url Url { get; private set; }

        public static Photo Create(
            PublicId publicId,
            Url url)
        {
            Photo photo = new Photo(
                Guid.NewGuid(),
                publicId,
                url);

            return photo;
        }
    }
}
