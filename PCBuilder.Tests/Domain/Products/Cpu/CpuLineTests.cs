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
        static Manufacturer manufacturer = new Manufacturer(
            name: "AMD",
            website: "https://www.amd.com/",
            twitterProfile: "@AMD",
            facebookProfile: "AMD",
            instagramProfile: "amd",
            warrantyQualityModifier: 1.5m,
            motherboardDefaultWarranty: 24.0m,
            motherboardDefaultBiosValueFactor: 0.9m,
            memoryDefaultOverallQualityFactor: 1.2m,
            videoCardDefaultWarranty: 36.0m
        );

        static CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture("wqeeqeq", manufacturer, "weqweq");

        static CpuSocket cpuSocket = new CpuSocket("LGA1200", manufacturer);

        CpuLine cpuLine1 = new CpuLine(
            codename: "Core i9-10900K",
            microarchitecture: cpuMicroarchitecture,
            socket: cpuSocket,
            motherboardPricesValueModifier: 5.0m,
            stabilityValueFactor: 8.0m
        );

        [Fact]
        public void TestIfCpuLineIsEquals()
        {
            // Arrange
            CpuLine cpuLine2 = new CpuLine(
                codename: "Core i9-10900K",
                microarchitecture: cpuMicroarchitecture,
                socket: cpuSocket,
                motherboardPricesValueModifier: 5.0m,
                stabilityValueFactor: 8.0m
            );

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