using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.MotherboardAndMemory;

namespace PCBuilder.Tests
{
    public class MemoryModuleConfigurationTests
    {

        static MemoryModuleConfiguration memoryModuleConfiguration = new MemoryModuleConfiguration(10, 10.0m);

        [Fact]
        public void TestIfMemoryModuleIsEqualsTo()
        {
            MemoryModuleConfiguration memoryModuleConfiguration2 = new MemoryModuleConfiguration(10, 10.0m);

            var expected = memoryModuleConfiguration.Equals(memoryModuleConfiguration2);

            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfMemoryModuleIsNotEqualsTo()
        {
            MemoryModuleConfiguration memoryModuleConfiguration2 = new MemoryModuleConfiguration(11, 11.1m);

            var expected = memoryModuleConfiguration.Equals(memoryModuleConfiguration2);

            expected.Should().BeFalse();
        }

        [Fact]
        public void TestIfTotalCapacityIsMatching()
        {
            var expected = memoryModuleConfiguration.Quantity * memoryModuleConfiguration.Capacity;

            memoryModuleConfiguration.TotalCapacity.Should().Be(expected);
        }
    }
}