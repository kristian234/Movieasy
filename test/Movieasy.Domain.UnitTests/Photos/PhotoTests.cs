using FluentAssertions;
using Movieasy.Domain.Photos;
using Movieasy.Domain.UnitTests.Infrastructure;

namespace Movieasy.Domain.UnitTests.Photos
{
    public class PhotoTests : BaseTest
    {
        [Fact]
        public void Create_Should_SetValues()
        {
            // Act
            Photo photo = Photo.Create(PhotoData.PublicId, PhotoData.Url);

            // Assert
            photo.PublicId.Should().Be(PhotoData.PublicId);
            photo.Url.Should().Be(PhotoData.Url);
        }

        [Fact]
        public void Update_Should_SetValues()
        {
            // Act
            Photo photo = Photo.Create(PhotoData.PublicId, PhotoData.Url);

            const string updatedValue = "updated";

            photo.Update(updatedValue, updatedValue);

            // Assert
            photo.PublicId.Value.Should().Be(updatedValue);
            photo.Url.Value.Should().Be(updatedValue);
        }
    }
}
