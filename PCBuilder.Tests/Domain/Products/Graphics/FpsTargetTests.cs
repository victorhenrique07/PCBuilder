using System;
using FluentAssertions;
using Xunit;
using PCBuilder.Domain.Products.Graphics;

namespace PCBuilder.Tests
{
    public class FpsTargetTests
    {   
        [Fact]
        public void TestIf60FpsIsMedium()
        {

            var expect = FpsTarget.FindFromAvailable(60);

            expect.Should().Be(FpsTarget.Medium);
        }

        [Fact]
        public void TestIf120FpsIsHigh()
        {

            var expect = FpsTarget.FindFromAvailable(120);

            expect.Should().Be(FpsTarget.High);
        }

        [Fact]
        public void TestIf240FpsIsVeryHigh()
        {

            var expect = FpsTarget.FindFromAvailable(240);

            expect.Should().Be(FpsTarget.VeryHigh);
        }
    }
}