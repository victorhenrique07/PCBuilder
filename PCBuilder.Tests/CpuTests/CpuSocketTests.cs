using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Shared;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;
using PCBuilder.Domain.Products.MotherboardAndMemory;

namespace PCBuilder.Tests
{
    public class CpuSocketTests
    {
        static Manufacturer manufacturer = new Manufacturer("string name",
                                                    "string website",
                                                    "string twitterProfile",
                                                    "string facebookProfile",
                                                    "string instagramProfile", 
                                                    10.0m, 10.0m, 10.0m, 10.0m, 10.0m);

        static MemoryType memoryType = new MemoryType(MemoryChipType.Gddr5, MemoryModuleType.Dimm288Pin);

        static CpuSocket cpuSocket = new CpuSocket("Cpu", manufacturer);

        [Fact]
        public void TestIfCpuSocketIsEquals()
        {
            // Arrange
            CpuSocket cpuSocket2 = new CpuSocket("Cpu", manufacturer);

            // Act
            var expected = cpuSocket.Equals(cpuSocket2);

            // Assert
            cpuSocket2.Name.Should().NotBeNull();
            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfCpuSocketIsNotEquals()
        {
            // Arrange
            CpuSocket cpuSocket2 = new CpuSocket("Cpu2", manufacturer);

            // Act
            var expected = cpuSocket.Equals(cpuSocket2);

            // Assert
            cpuSocket2.Name.Should().NotBeNull();
            expected.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void TestIfCpuSocketIsThrowingArgumentNullException(string nullName)
        {
            Action cpuSocket2 = () => new CpuSocket(nullName, manufacturer);
            Action cpuSocket3 = () => new CpuSocket("Cpu3", null);

            cpuSocket2.Should().Throw<ArgumentNullException>();
            cpuSocket3.Should().Throw<ArgumentNullException>();
        }
    }
}