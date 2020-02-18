using FluentAssertions;
using PuppyApi.Business.Validation;
using PuppyApi.CrossDomain.TestHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PuppyApi.Business.Tests.Validation
{
    public class DateEntryValidatorTests : TestsFor<DateEntryValidator>
    {
        [Fact]
        public void IsValid_DateIsTwoDaysAhead_ReturnsFalse()
        {
            // Arrange
            var twoDaysFromNow = DateTime.Now.AddDays(2);

            // Act
            var result = Instance.IsValid(twoDaysFromNow);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_DateIsTwoDaysBehind_ReturnsFalse()
        {
            // Arrange
            var twoDaysFromNow = DateTime.Now.AddDays(-2);

            // Act
            var result = Instance.IsValid(twoDaysFromNow);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_DateIsTwoHoursBehind_ReturnsTrue()
        {
            // Arrange
            var twoHoursFromNow = DateTime.Now.AddHours(-2);

            // Act
            var result = Instance.IsValid(twoHoursFromNow);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValid_DateIsOneHourAhead_ReturnsFalse()
        {
            // Arrange
            var oneHourFromNow = DateTime.Now.AddHours(1);

            // Act
            var result = Instance.IsValid(oneHourFromNow);

            // Assert
            result.Should().BeFalse();
        }
    }
}
