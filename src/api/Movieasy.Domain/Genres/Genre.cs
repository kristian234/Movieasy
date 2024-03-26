using Movieasy.Domain.Abstractions;

namespace Movieasy.Domain.Genres
{
    public sealed class Genre : Entity
    {
        private Genre(
            Guid id,
            Name name) : base(id)
        {
            Name = name;
        }

        public Name Name { get; private set; }

        public static Genre Create(
            Name name)
        {
            Genre genre = new Genre(
                Guid.NewGuid(),
                name);

            return genre;   
        }

    }
}
