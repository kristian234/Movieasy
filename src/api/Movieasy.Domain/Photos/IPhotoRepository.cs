namespace Movieasy.Domain.Photos
{
    public interface IPhotoRepository
    {
        public Task<Photo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(Photo photo);
        public void Remove(Photo photo);
    }
}
