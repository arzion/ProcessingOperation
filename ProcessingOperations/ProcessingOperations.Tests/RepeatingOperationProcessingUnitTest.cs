using System;
using System.Threading;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using ProcessingOperations.Tests.TestInfrastructure;
using Xunit.Extensions;

namespace ProcessingOperations.Tests
{
    public class RepeatingOperationProcessingManagerTest
    {
        [Theory, AutoMockData]
        public void WhenStarting_ThenExceptionThrown_IfStartCalledTwice(
           [Frozen]RepeatingOperationSettings settings,
           double anyInterval,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = anyInterval;

            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            // Act
            sut.Start();
            Action secondStartAction = () => sut.Start();

            // Assert
            secondStartAction.ShouldThrow<InvalidOperationException>();
        }

        [Theory, AutoMockData]
        public void WhenStarting_ThenThrowsException_IfStartCalledAfterDispose(
           [Frozen]RepeatingOperationSettings settings,
           double anyInterval,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = anyInterval;

            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            // Act
            sut.Start();
            sut.Dispose();
            Action secondStartAction = () => sut.Start();

            // Assert
            secondStartAction.ShouldThrow<ObjectDisposedException>();
        }

        [Theory, AutoMockData]
        public void WhenDisposing_ThenRepeatingWasStopped(
           [Frozen]Mock<IOperation> operationMock,
           [Frozen]RepeatingOperationSettings settings,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = 0.1;

            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            // Act
            sut.Start();
            Thread.Sleep(500);
            sut.Dispose();
            Thread.Sleep(500);

            // Assert
            operationMock.Verify(
                operation => operation.Execute(It.IsAny<CancellationToken>()),
                Times.Between(4, 6, Range.Inclusive));
        }

        [Theory, AutoMockData]
        public void WhenDisposing_ThenOperationStopsExecution_IfOperationIsCancellable(
           [Frozen]RepeatingOperationSettings settings,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = 0.1;

            fixture.Register<IOperation>(() => new EndlessCancellableOperationStub());
            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            Exception raisedException = null;
            var eventExecutionCount = 0;
            sut.OnExecutionError += exception =>
            {
                raisedException = exception;
                eventExecutionCount++;
            };

            // Act
            sut.Start();
            Thread.Sleep(750);
            sut.Dispose();
            Thread.Sleep(1000);

            // Assert
            raisedException.Should().BeAssignableTo<TimeoutException>();
            eventExecutionCount.Should().Be(1);
        }

        [Theory, AutoMockData]
        public void WhenDisposingImmidiately_ThenOperationStopsExecution_IfOperationIsCancellable(
           [Frozen]RepeatingOperationSettings settings,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = 0.1;

            fixture.Register<IOperation>(() => new EndlessCancellableOperationStub());
            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            Exception raisedException = null;
            var eventExecutionCount = 0;
            sut.OnExecutionError += exception =>
            {
                raisedException = exception;
                eventExecutionCount++;
            };

            // Act
            sut.Start();
            Thread.Sleep(10);
            sut.Dispose();
            Thread.Sleep(1000);

            // Assert
            raisedException.Should().BeNull();
            eventExecutionCount.Should().Be(0);
        }

        [Theory, AutoMockData]
        public void WhenDisposing_ThenWaitingForOperation_IfOperationIsNotCancellable(
           [Frozen]RepeatingOperationSettings settings,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = 0.1;

            fixture.Register<IOperation>(() => new EndlessNotCancellableOperationStub());
            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            Exception raisedException = null;
            var eventExecutionCount = 0;
            sut.OnExecutionError += exception =>
            {
                raisedException = exception;
                eventExecutionCount++;
            };

            // Act
            sut.Start();
            Thread.Sleep(750);
            sut.Dispose();
            Thread.Sleep(1000);

            // Assert
            raisedException.Should().BeAssignableTo<TimeoutException>();
            eventExecutionCount.Should().Be(2);
        }

        [Theory, AutoMockData]
        public void WhenDisposingImmidiately_ThenWaitingForOperation_IfOperationIsNotCancellable(
           [Frozen]RepeatingOperationSettings settings,
           IFixture fixture)
        {
            // Arrange
            settings.EverySeconds = 0.1;

            fixture.Register<IOperation>(() => new EndlessNotCancellableOperationStub());
            var sut = fixture.Create<RepeatingOperationProcessingManager>();

            Exception raisedException = null;
            var eventExecutionCount = 0;
            sut.OnExecutionError += exception =>
            {
                raisedException = exception;
                eventExecutionCount++;
            };

            // Act
            sut.Start();
            Thread.Sleep(10);
            sut.Dispose();
            Thread.Sleep(1000);

            // Assert
            raisedException.Should().BeAssignableTo<TimeoutException>();
            eventExecutionCount.Should().Be(1);
        }

        #region nested classes

        private class EndlessCancellableOperationStub : IOperation
        {
            public void Execute(CancellationToken token)
            {
                var startedTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now - startedTime > TimeSpan.FromMilliseconds(500))
                    {
                        throw new TimeoutException();
                    }

                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
        }

        private class EndlessNotCancellableOperationStub : IOperation
        {
            public void Execute(CancellationToken token)
            {
                var startedTime = DateTime.Now;
                while (true)
                {
                    if (DateTime.Now - startedTime > TimeSpan.FromMilliseconds(500))
                    {
                        throw new TimeoutException();
                    }
                }
            }
        }

        #endregion
    }
}