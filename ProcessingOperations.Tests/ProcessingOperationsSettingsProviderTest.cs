using System.Linq;
using FluentAssertions;
using ProcessingOperations.Configuration;
using Xunit;

namespace ProcessingOperations.Tests
{
    public class ProcessingOperationsSettingsProviderTest
    {
        [Fact]
        public void WhenGettingSettings_ThenSettingsProvided()
        {
            // Act
            var sut = new ProcessingOperationsSettingsProvider();
            var settings = sut.GetSettings();

            // Arrange
            settings.RepeatingOperationProcessingManagers.Should().HaveCount(2);
            var manager1 = settings.RepeatingOperationProcessingManagers.ElementAt(0);
            manager1.EverySeconds.Should().Be(0.1);
            manager1.Name.Should().Be("FakeProcessingManagerName1");
            manager1.OperationKey.Should().Be("FakeOperationKey1");

            var manager2 = settings.RepeatingOperationProcessingManagers.ElementAt(1);
            manager2.EverySeconds.Should().Be(0.2);
            manager2.Name.Should().Be("FakeProcessingManagerName2");
            manager2.OperationKey.Should().Be("FakeOperationKey2");
        }
    }
}