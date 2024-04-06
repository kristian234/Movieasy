using Movieasy.Domain.Movies;

namespace Movieasy.Domain.Genres
{
    public record Name(string Value)
    {
        public static explicit operator string(Name name) => name.Value;
    };
}
