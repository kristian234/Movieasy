namespace Movieasy.Domain.Actors
{
    public record Biography(string Value)
    {
        public static explicit operator string(Biography name) => name.Value;
    };
}
