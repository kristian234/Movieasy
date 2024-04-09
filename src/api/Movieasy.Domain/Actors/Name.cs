namespace Movieasy.Domain.Actors
{
    public record Name(string Value)
    {
        public static explicit operator string(Name name) => name.Value;
    };
}
