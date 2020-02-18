using FluentAssertions;
using PuppyApi.Business.Handlers;
using PuppyApi.CrossDomain.TestHelpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PuppyApi.Business.Tests.Handlers
{
    public class ExceptionHandlerTests : TestsFor<ExceptionHandler>
    {
        [Fact]
        public async Task GetAsync_TaskIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<Task<int>> nullFunc = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Instance.GetAsync(nullFunc));
        }

        [Fact]
        public async Task GetAsync_FunctionReturnsValue_ResultIsThatValue()
        {
            // Arrange
            Func<Task<int>> validFunc = () => Task.FromResult(10);

            // Act
            var result = await Instance.GetAsync(validFunc);

            // Assert
            result.Should().Be(10);
        }

        [Fact]
        public async Task GetAsync_FunctionThrows_ResultIsDefault()
        {
            // Arrange
            var badException = new Exception("I'm bad");
            Func<Task<int>> throwingFunc = () => throw badException;

            // Act
            var result = await Instance.GetAsync(throwingFunc);

            // Assert
            result.Should().Be(default);
        }

        [Fact]
        public async Task RunAsync_FunctionIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<Task> nullFunction = null;

            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(() => Instance.RunAsync(nullFunction));
        }

        [Fact]
        public async Task RunAsync_FunctionExecutes_ResultCanBeVerified()
        {
            // Arrange
            bool flagWasSet = false;
            Func<Task> setFlagFunction = () => Task.Run(() => flagWasSet = true);

            // Act
            await Instance.RunAsync(setFlagFunction);

            // Assert
            flagWasSet.Should().BeTrue();
        }

        [Fact]
        public async Task RunAsync_FunctionThrows_StillReturns()
        {
            // Arrange
            var badException = new Exception("I'm bad");
            Func<Task> badFunction = () => throw badException;

            // Act
            await Instance.RunAsync(badFunction);

            // Assert (nothing to do, it shouldn't fail)
        }
    }
}
