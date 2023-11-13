using System;
using FluentAssertions;
using Xunit;
using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Graphics;

namespace PCBuilder.Tests
{

    public class GraphicsMemoryTests
    {
        
        static GraphicsMemory graphicsMemory = new GraphicsMemory(MemoryChipType.Ddr4, 10.0m, 3666m, 10);

        [Fact]
        public void TestIfGraphicsMemoryIsEqualsTo()
        {
            GraphicsMemory graphicsMemory2 = new GraphicsMemory(MemoryChipType.Ddr4, 10.0m, 3666m, 10);

            var expect = graphicsMemory.Equals(graphicsMemory2);

            expect.Should().BeTrue();
        }

        [Fact]
        public void TestIfGraphicsMemoryIsNotEqualsTo()
        {
            GraphicsMemory graphicsMemory2 = new GraphicsMemory(MemoryChipType.Ddr5, 10.0m, 3666m, 10);

            var expect = graphicsMemory.Equals(graphicsMemory2);

            expect.Should().BeFalse();
        }

        [Fact]
        public void TestIfBandWidthIsMatching()
        {
            var expect = graphicsMemory.Frequency * graphicsMemory.BusWidth / 8m;

            expect.Should().Be(graphicsMemory.Bandwidth);
        }
    }
}