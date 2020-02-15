using Moq;
using PuppyApi.Business.Managers;
using PuppyApi.CrossDomain.TestHelpers;
using PuppyApi.Domain.Contracts.Handlers;
using PuppyApi.Domain.Contracts.Repositories;
using FluentAssertions;
using System;
using Xunit;

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
    }
}
