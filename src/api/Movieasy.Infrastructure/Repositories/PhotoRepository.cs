using Movieasy.Domain.Photos;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
