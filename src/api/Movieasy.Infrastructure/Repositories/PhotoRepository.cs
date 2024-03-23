using Movieasy.Domain.Photos;

namespace Movieasy.Infrastructure.Repositories
{
    internal sealed class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private readonly ApplicationDbContext _context;
        public PhotoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Remove(Photo photo)
        {
            _context.Remove(photo);
        }


    }
}
