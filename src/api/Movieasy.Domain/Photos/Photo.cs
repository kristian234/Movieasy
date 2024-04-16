using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Photos
{
    public sealed class Photo : Entity
    {
        private Photo() { }

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

        public Result Update(
            string publicId,
            string url)
        {
            PublicId newPublicId = new PublicId(publicId);
            Url newUrl = new Url(url);

            PublicId = newPublicId;
            Url = newUrl;

            return Result.Success();
        }
    }
}
