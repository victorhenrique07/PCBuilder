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
    }
}