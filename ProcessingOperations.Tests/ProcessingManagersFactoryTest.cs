using System.Threading;
using FluentAssertions;
using Moq;
using Xunit;

namespace ProcessingOperations.Tests
{
    public class ProcessingManagersFactoryTest
    {
        [Fact]
        public void WhenCreatingProcessingManagers_ThenProcessingManagersCreated()
        {
            // Arrange
            var operationFactoryMock = new Mock<IOperationFactory>();

            var firstOperationMock = new Mock<IOperation>();
            var secondOperationMock = new Mock<IOperation>();
            operationFactoryMock
                .Setup(factory => factory.Create("FakeOperationKey1"))
                .Returns(firstOperationMock.Object);
            operationFactoryMock
                .Setup(factory => factory.Create("FakeOperationKey2"))
                .Returns(secondOperationMock.Object);

            ProcessingOperationsConfiguration.SetOperationFactory(operationFactoryMock.Object);

            // Act
            var sut = new ProcessingManagersFactory();
            var managers = sut.Create();

            // Arrange
            managers.Should().HaveCount(2);
            foreach (var processingManager in managers)
            {
                processingManager.Should().NotBeNull();
            }
        }

        [Fact]
        public void WhenCreatingProcessingManagerByName_ThenProcessingManagerIsCreated()
        {
            // Arrange
            var operationFactoryMock = new Mock<IOperationFactory>();

            var firstOperationMock = new Mock<IOperation>();
            var secondOperationMock = new Mock<IOperation>();
            operationFactoryMock
                .Setup(factory => factory.Create("FakeOperationKey1"))
                .Returns(firstOperationMock.Object);
            operationFactoryMock
                .Setup(factory => factory.Create("FakeOperationKey2"))
                .Returns(secondOperationMock.Object);

            ProcessingOperationsConfiguration.SetOperationFactory(operationFactoryMock.Object);

            // Act
            var sut = new ProcessingManagersFactory();
            var manager1 = sut.Create("FakeProcessingManagerName1");
            var manager2 = sut.Create("FakeProcessingManagerName2");

            // Arrange
            manager1.Should().NotBeNull();
            manager2.Should().NotBeNull();
        }

        [Fact]
        public void WhenCreatingAndStartingProcessingManagers_ThenOperationsExecuted()
        {
            // Arrange
            var operationFactoryMock = new Mock<IOperationFactory>();

            var firstOperationMock = new Mock<IOperation>();
            var secondOperationMock = new Mock<IOperation>();
            operationFactoryMock
                .Setup(factory => factory.Create("FakeOperationKey1"))
                .Returns(firstOperationMock.Object);
            operationFactoryMock
                .Setup(factory => factory.Create("FakeOperationKey2"))
                .Returns(secondOperationMock.Object);

            ProcessingOperationsConfiguration.SetOperationFactory(operationFactoryMock.Object);

            // Act
            var sut = new ProcessingManagersFactory();
            var managers = sut.Create();
            foreach (var processingManager in managers)
            {
                processingManager.Start();
            }
            Thread.Sleep(1000);
            foreach (var processingManager in managers)
            {
                processingManager.Dispose();
            }
            Thread.Sleep(1000);

            // Arrange
            firstOperationMock.Verify(
                operation => operation.Execute(It.IsAny<CancellationToken>()),
                Times.Between(9, 11, Range.Inclusive));
            secondOperationMock.Verify(
                operation => operation.Execute(It.IsAny<CancellationToken>()),
                Times.Between(4, 6, Range.Inclusive));
        }
    }
}