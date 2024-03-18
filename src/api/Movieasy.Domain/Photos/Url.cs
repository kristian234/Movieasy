
namespace Movieasy.Domain.Photos
{
    public record Url(string Value)
    {
        public static explicit operator string(Url url) => url.Value;
    }
}
