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
using PuppyApi.Domain.Contracts.Validation;

namespace PuppyApi.Business.Tests.Managers
{
    public class PottyBreaksManagerTests : TestsFor<PottyBreaksManager>
    {
        [Fact]
        public void Constructor_ExceptionHandlerIsNull_ThrowsException()
        {
            // Arrange
            IExceptionHandler nullHandler = null;
            var repoMock = new Mock<IPottyBreakRepository>();
            var validatorMock = new Mock<IValidator<DateTime>>();
            
            // Act
            Assert.Throws<ArgumentNullException>(() => Instance = new PottyBreaksManager(nullHandler, repoMock.Object, validatorMock.Object));
        }

        [Fact]
        public void Constructor_ValidatorIsNull_ThrowsException()
        {
            // Arrange
            var exeHandlerMock = new Mock<IExceptionHandler>();
            var repoMock = new Mock<IPottyBreakRepository>();
            IValidator<DateTime> nullValidator = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => Instance = new PottyBreaksManager(exeHandlerMock.Object, repoMock.Object, nullValidator));
        }


        [Fact]
        public void Constructor_RepositoryIsNull_ThrowsException()
        {
            // Arrange
            var exeHandlerMock = new Mock<IExceptionHandler>();
            var validatorMock = new Mock<IValidator<DateTime>>();
            IPottyBreakRepository nullRepo = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => Instance = new PottyBreaksManager(exeHandlerMock.Object, nullRepo, validatorMock.Object));
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
            var result = await Instance.GetAllAsync(20);

            // Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllAsync_RepositoryReturnsTenPottyBreaks_ResultIsTenPottyBreaks()
        {
            // Arrange
            int numberToGet = 10;
            EnsureExceptionHandlerIsReal();
            var tenPottyBreaks = Builder<PottyBreak>
                .CreateListOfSize(numberToGet)
                .Build()
                .AsEnumerable();

            GetMockFor<IPottyBreakRepository>()
                .Setup(r => r.GetAllAsync(numberToGet))
                .Returns(Task.FromResult(tenPottyBreaks));

            // Act
            var results = await Instance.GetAllAsync(numberToGet);

            // Assert
            results.Count().Should().Equals(numberToGet);
        }

        [Fact]
        public async Task DeleteAsync_PottyBreakIsNull_InstantlyReturns()
        {
            // Arrange
            var anyFunction   = It.IsAny<Func<Task>>();
            var anyPottyBreak = It.IsAny<PottyBreak>();
            var anyDateTime   = It.IsAny<DateTime>();

            // Act
            await Instance.DeleteAsync(null);

            // Assert
            GetMockFor<IExceptionHandler>().Verify(h => h.RunAsync(anyFunction), Times.Never());
            GetMockFor<IPottyBreakRepository>().Verify(r => r.DeleteAsync(anyPottyBreak), Times.Never());
            GetMockFor<IValidator<DateTime>>().Verify(v => v.IsValid(anyDateTime), Times.Never());
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

        [Fact]
        public async Task SaveAsync_DateValidationFails_DoesNotSave()
        {
            // Arrange
            var anyDate = It.IsAny<DateTime>();
            EnsureExceptionHandlerIsReal();
            GetMockFor<IValidator<DateTime>>().Setup(v => v.IsValid(anyDate)).Returns(false);
            var pottyBreak = Builder<PottyBreak>.CreateNew().Build();

            // Act
            await Instance.SaveAsync(pottyBreak);

            // Assert
            GetMockFor<IPottyBreakRepository>().Verify(r => r.SaveAsync(pottyBreak), Times.Never());
        }


        #region Helpers

        private void EnsureExceptionHandlerIsReal()
        {            
            InjectSingle<IExceptionHandler>(new ExceptionHandler());
            var anyDateTime = It.IsAny<DateTime>();
            GetMockFor<IValidator<DateTime>>().Setup(v => v.IsValid(anyDateTime)).Returns(true);
        }

        #endregion
    }
}
