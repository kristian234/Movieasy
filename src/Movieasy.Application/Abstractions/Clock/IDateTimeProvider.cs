namespace Movieasy.Application.Abstractions.Clock
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
