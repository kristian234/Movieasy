using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Genres
{
    public static class GenreErrors
    {
        public static Error NotFound = new Error(
            "Genre.NotFound",
            "The genre with the specified identified couldn't be found");
    }
}
