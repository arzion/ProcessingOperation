using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace ProcessingOperations.Tests.TestInfrastructure
{
    public class AutoFixtureCustomization : CompositeCustomization
    {
        public AutoFixtureCustomization()
            : base(
                new StableFiniteSequenceCustomization(),
                new MultipleCustomization(),
                new AutoMoqCustomization())
        {
        }
    }
}