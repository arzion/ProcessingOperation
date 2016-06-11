using Ploeh.AutoFixture.Xunit;

namespace ProcessingOperations.Tests.TestInfrastructure
{
    public class AutoMockDataAttribute : AutoDataAttribute
    {
        public AutoMockDataAttribute()
            : base(new TestAutoFixture())
        {
        }
    }
}