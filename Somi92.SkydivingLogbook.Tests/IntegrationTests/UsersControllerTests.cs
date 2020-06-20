using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Somi92.SkydivingLogbook.Core.Features.Users;
using Somi92.SkydivingLogbook.Tests.IntegrationTests.Fixtures;
using Xunit;

namespace Somi92.SkydivingLogbook.Tests.IntegrationTests
{
    [Collection("IntegrationTestsCollection")]
    [Trait("category", "integration")]
    public class UsersControllerTests
    {
        protected IntegrationTestFixture Fixture;

        public UsersControllerTests(IntegrationTestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task GetUser_ValidId_ReturnsResponse()
        {
            // Arrange

            // Act
            var response = await Fixture.Client.GetAsync("Users/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<LoadUserData.Result>(json);
            result.Should().BeEquivalentTo(new LoadUserData.Result { Id = 1, Name = "John Doe" });
        }

        // TODO: Exception handling
    }
}
