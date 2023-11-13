using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Peripherals;

namespace PCBuilder.Tests
{
    public class DisplayResolutionTests
    {
        static DisplayResolution displayResolution = new DisplayResolution("FHD", 1920, 1080);

        [Fact]
        public void TestIfResolutionIsEqualsTo()
        {
            DisplayResolution displayResolution2 = new DisplayResolution("FHD", 1920, 1080);

            var expected = displayResolution.Equals(displayResolution2);

            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfResolutionIsNotEqualsTo()
        {
            DisplayResolution displayResolution2 = new DisplayResolution("QHD", 2560, 1440);

            var expected = displayResolution.Equals(displayResolution2);

            expected.Should().BeFalse();
        }

        [Fact]
        public void TestIfPixelsIsMatching()
        {
            displayResolution.Pixels.Should().Be(displayResolution.Columns * displayResolution.Rows);
        }

        [Fact]
        public void TestIfDescriptionIsReturningNameColumnsRows()
        {
            displayResolution.Description.Should().Be($"{displayResolution.Name} ({displayResolution.Columns} x {displayResolution.Rows})");
        }

        [Fact]
        public void TestIfDescriptionIsEqualsToString()
        {
            displayResolution.Description.Should().Be(displayResolution.ToString());
        }
    }
}