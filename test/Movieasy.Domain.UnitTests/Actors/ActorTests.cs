using FluentAssertions;
using Movieasy.Domain.Actors;
using Movieasy.Domain.UnitTests.Infrastructure;

namespace Movieasy.Domain.UnitTests.Actors
{
    public class ActorTests : BaseTest
    {
        [Fact]
        public void Create_ShouldSetValues()
        {
            // Act
            Actor actor = Actor.Create(
                ActorData.Name,
                ActorData.Biography);

            // Assert
            actor.Name.Should().Be(ActorData.Name);
            actor.Biography.Should().Be(ActorData.Biography);
        }

        [Fact]
        public void Update_ShouldSetValues()
        {
            // Act
            Actor actor = Actor.Create(
              ActorData.Name,
              ActorData.Biography);

            const string updatedName = "updatedName";
            const string updatedBiography = "updated biography";

            actor.Update(updatedName, updatedBiography);
            
            // Assert
            actor.Name.Value.Should().Be(updatedName);
            actor.Biography.Value.Should().Be(updatedBiography);
        }
    }
}
