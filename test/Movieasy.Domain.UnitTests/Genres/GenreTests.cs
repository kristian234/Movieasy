using FluentAssertions;
using Movieasy.Domain.Genres;
using Movieasy.Domain.UnitTests.Infrastructure;
using System.Text.RegularExpressions;

namespace Movieasy.Domain.UnitTests.Genres
{
    public class GenreTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetValues()
        {
            // Act
            Genre genre = Genre.Create(
                GenreData.Name);

            // Assert
            genre.Name.Should().Be(GenreData.Name);
        }

        [Fact]
        public void Update_Should_SetValues()
        {
            // Act
            Genre genre = Genre.Create(
                GenreData.Name);

            const string updateName = "updated name";
            genre.Update(updateName);

            // Assert
            genre.Name.Value.Should().Be(updateName);
        }
    }
}
