using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Shared;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine;

namespace PCBuilder.Tests
{
    public class CpuLineTests
    {
        static Manufacturer manufacturer = new Manufacturer("string name",
                                                    "string website",
                                                    "string twitterProfile",
                                                    "string facebookProfile",
                                                    "string instagramProfile", 
                                                    10.0m, 10.0m, 10.0m, 10.0m, 10.0m);

        static CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture("wqeeqeq", manufacturer, "weqweq");

        static CpuSocket cpuSocket = new CpuSocket("daskljdaslk", manufacturer);

        CpuLine cpuLine1 = new CpuLine("Cpu1", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);

        [Fact]
        public void TestIfCpuLineIsEquals()
        {
            // Arrange
            CpuLine cpuLine2 = new CpuLine("Cpu1", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);

            // Act
            var expected = cpuLine1.Equals(cpuLine2);

            // Assert
            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfCpuLineIsNotEquals()
        {
            // Arrange
            CpuLine cpuLine2 = new CpuLine("Cpu2", cpuMicroarchitecture, cpuSocket, 10.10m, 10.10m);

            // Act
            var expected = cpuLine1.Equals(cpuLine2);

            // Assert
            cpuLine2.Codename.Should().NotBeNull();
            expected.Should().BeFalse();
        }
    }
}