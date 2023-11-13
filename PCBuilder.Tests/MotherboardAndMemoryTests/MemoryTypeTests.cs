using System;
using FluentAssertions;
using PCBuilder.Domain.Products.MotherboardAndMemory;

namespace PCBuilder.Tests
{
    public class MemoryTypeTests
    {

        static MemoryType memoryType = new MemoryType(MemoryChipType.Ddr4, MemoryModuleType.Dimm184Pin);

        [Fact]
        public void TestIfMemoryTypeIsEqualsTo()
        {
            MemoryType memoryType2 = new MemoryType(MemoryChipType.Ddr4, MemoryModuleType.Dimm184Pin);

            var expected = memoryType.Equals(memoryType2);

            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfMemoryTypeIsNotEqualsTo()
        {
            MemoryType memoryType2 = new MemoryType(MemoryChipType.Ddr5, MemoryModuleType.Dimm184Pin);

            var expected = memoryType.Equals(memoryType2);

            expected.Should().BeFalse();
        }

        [Fact]
        public void TestIfNameIsUpperCase()
        {
            var expected = "DDR4";

            memoryType.Name.Should().Be(expected);
        }
    }
}