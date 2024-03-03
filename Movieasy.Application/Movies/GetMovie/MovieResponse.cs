namespace Movieasy.Application.Movies.GetMovie
{
    public sealed class MovieResponse
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string? ReleaseDate { get; init; }
        public string Rating { get; init; } = string.Empty;
        public double Duration { get; init; }
    }
}
