namespace Movieasy.Domain.Movies
{
    public record Title(string Value)
    {
        public static explicit operator string(Title title) => title.Value;
    };
}
