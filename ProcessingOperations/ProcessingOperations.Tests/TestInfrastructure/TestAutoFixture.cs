using Ploeh.AutoFixture;

namespace ProcessingOperations.Tests.TestInfrastructure
{
    public class TestAutoFixture : Fixture
    {
        public TestAutoFixture()
        {
            Customize(new AutoFixtureCustomization());
        }
    }
}