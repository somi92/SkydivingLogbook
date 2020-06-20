using Somi92.SkydivingLogbook.Tests.IntegrationTests.Fixtures;
using Xunit;

namespace Somi92.SkydivingLogbook.Tests.IntegrationTests
{
    [CollectionDefinition("IntegrationTestsCollection")]
    public class IntegrationTestCollection : ICollectionFixture<IntegrationTestFixture>
    {
    }
}
