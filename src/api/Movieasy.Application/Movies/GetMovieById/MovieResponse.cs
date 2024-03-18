namespace Movieasy.Application.Movies.GetMovieById
{
    public sealed class MovieResponse
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string? ReleaseDate { get; init; }
        public string UploadDate { get; init; } = string.Empty;
        public string Rating { get; init; } = string.Empty;
        public double Duration { get; init; }
        public string ImageUrl { get; init; } = string.Empty;
    }
}
