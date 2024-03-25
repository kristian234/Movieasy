using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Movies
{
    public static class MovieErrors
    {
        public static Error NotFound = new Error(
            "Movie.NotFound",
            "The movie with the specified identifier was not found");

        public static Error UpdateFailed = new Error(
            "Movie.UpdateFailed",
            "An error occurred while attempting to update the movie");

        public static Error DeleteFailed = new Error(
            "Movie.DeleteFailed",
            "An unexpected error occured while attempting to delete the movie");
    }
}
