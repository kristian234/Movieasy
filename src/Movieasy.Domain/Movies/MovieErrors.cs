using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Movies
{
    public static class MovieErrors
    {
        public static Error NotFound = new Error(
            "Movie.NotFound",
            "The movie with the specified identifier was not found");
    }
}
