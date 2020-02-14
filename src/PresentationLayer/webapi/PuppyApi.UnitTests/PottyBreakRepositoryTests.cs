using FluentAssertions;
using PuppyApi.CrossDomain.TestHelpers;
using PuppyApi.Data;
using PuppyApi.Domain.Entities;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace PuppyApi.UnitTests
{    
    
    public class PottyBreakRepositoryTests : TestsFor<IPottyBreakRepository>
    {
        private string TestableGuid = "69aae741-2020-4119-ae98-060cdba2e587";
        private PottyBreak TestablePottyBreak;

        public PottyBreakRepositoryTests()
        {
            TestablePottyBreak = new PottyBreak
            {
                Id = Guid.Parse(TestableGuid),
                DateTime = DateTime.Now,
                Peed = true,
                Pooed = false,
                Comment = "For Testing"
            };           
        }

        [Fact, Order(1), Trait("Category", "Integration")]
        public async Task InitializeAsync_WhenCalled_DoesNotThrow()
        {
            // Act
            await Instance.InitializeAsync();
        }

        [Fact, Order(2), Trait("Category", "Integration")]
        public async Task SaveAsync_WhenCalled_IsRetrievedWithGetById()
        {
            // Act
            await Instance.SaveAsync(TestablePottyBreak);

            // Assert
            var match = await Instance.GetById(TestablePottyBreak.Id);
            match.Should().Equals(TestablePottyBreak);
        }

        [Fact, Order(3), Trait("Category", "Integration")]
        public async Task GetAllAsync_WhenCalled_ReturnsAtLeastOnePottyBreak()
        {
            // Arrange
            await Instance.SaveAsync(TestablePottyBreak);

            // Act
            var result = await Instance.GetAllAsync();

            // Assert
            result.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact, Order(4), Trait("Category", "Integration")]
        public async Task DeleteAsync_WhenCalled_GetByIdReturnsNothing()
        {
            // Arrange
            await Instance.SaveAsync(TestablePottyBreak);

            // Act
            await Instance.DeleteAsync(TestablePottyBreak);

            // Assert
            var pottyBreak = await Instance.GetById(TestablePottyBreak.Id);
            pottyBreak.Should().BeNull();
        }
    }
}
