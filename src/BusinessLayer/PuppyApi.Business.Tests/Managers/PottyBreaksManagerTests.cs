using Moq;
using PuppyApi.Business.Managers;
using PuppyApi.CrossDomain.TestHelpers;
using PuppyApi.Domain.Contracts.Handlers;
using PuppyApi.Domain.Contracts.Repositories;
using FluentAssertions;
using System;
using Xunit;
using System.Threading.Tasks;
using PuppyApi.Business.Handlers;
using FizzWare.NBuilder;
using PuppyApi.Domain.Entities;
using System.Linq;

namespace PuppyApi.Business.Tests.Managers
{
    public class PottyBreaksManagerTests : TestsFor<PottyBreaksManager>
    {
        [Fact]
        public void Constructor_ExceptionHandlerIsNull_ThrowsExeption()
        {
            // Arrange
            IExceptionHandler nullHandler = null;
            var repoMock = new Mock<IPottyBreakRepository>();
            
            // Act
            Assert.Throws<ArgumentNullException>(() => Instance = new PottyBreaksManager(nullHandler, repoMock.Object));
        }

        [Fact]
        public void Constructor_RepositoryIsNull_ThrowsExeption()
        {
            // Arrange
            var exeHandlerMock = new Mock<IExceptionHandler>();
            IPottyBreakRepository nullRepo = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => Instance = new PottyBreaksManager(exeHandlerMock.Object, nullRepo));
        }

        [Fact]
        public void Constructor_ParametersAreValid_DoesNotThrow()
        {
            // ASsert            
            Instance.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllAsync_RepositoryThrowsError_ReturnsEmptyCollection()
        {
            // Arrange
            EnsureExceptionHandlerIsReal();

            // Act
            var result = await Instance.GetAllAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_RepositoryReturnsTenPottyBreaks_ResultIsTenPottyBreaks()
        {
            // Arrange
            EnsureExceptionHandlerIsReal();
            var tenPottyBreaks = Builder<PottyBreak>
                .CreateListOfSize(10)
                .Build()
                .AsEnumerable();

            GetMockFor<IPottyBreakRepository>()
                .Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult(tenPottyBreaks));

            // Act
            var results = await Instance.GetAllAsync();

            // Assert
            results.Count().Should().Equals(10);
        }

        [Fact]
        public async Task DeleteAsync_PottyBreakIsNull_InstantlyReturns()
        {
            // Arrange
            var anyFunction = It.IsAny<Func<Task>>();
            var anyPottyBreak = It.IsAny<PottyBreak>();

            // Act
            await Instance.DeleteAsync(null);

            // Assert
            GetMockFor<IExceptionHandler>().Verify(h => h.RunAsync(anyFunction), Times.Never());
            GetMockFor<IPottyBreakRepository>().Verify(r => r.DeleteAsync(anyPottyBreak), Times.Never());
        }

        [Fact]
        public async Task DeleteAsync_PottyBreakIsValid_ExecutesDelete()
        {
            // Arrange
            EnsureExceptionHandlerIsReal();
            var pottyBreak = Builder<PottyBreak>.CreateNew().Build();

            // Act
            await Instance.DeleteAsync(pottyBreak);

            // Assert
            GetMockFor<IPottyBreakRepository>().Verify(r => r.DeleteAsync(pottyBreak), Times.Once());
        }


        #region Helpers

        private void EnsureExceptionHandlerIsReal()
        {            
            InjectSingle<IExceptionHandler>(new ExceptionHandler());
        }

        #endregion
    }
}
