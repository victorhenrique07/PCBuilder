using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Shared;
using PCBuilder.Domain.Products.Cpus;

namespace PCBuilder.Tests
{
    public class SupportedCpuLineTests
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

        static CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture(
            codename: "Tiger Lake",
            manufacturer: manufacturer,
            manufacturingProcess: "10nm"
        );

        static CpuSocket cpuSocket = new CpuSocket("AM4", manufacturer);
        static CpuLine cpuLine = new CpuLine("Ryzen 5 5600", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);
        SupportedCpuLine supportedCpuLine = new SupportedCpuLine(cpuLine, false);

        [Fact]
        public void TestIfSupportedCpuLineIsEqualsTo()
        {
            SupportedCpuLine supportedCpuLine2 = new SupportedCpuLine(cpuLine, false);

            var expectedTrue = supportedCpuLine.Equals(supportedCpuLine2);

            expectedTrue.Should().BeTrue();
        }

        public void TestIfSupportedCpuLineIsNotEqualsTo()
        {
            SupportedCpuLine supportedCpuLine2 = new SupportedCpuLine(new CpuLine("Ryzen 7 5800X3D", cpuMicroarchitecture, cpuSocket, 10.1m, 11.1m), false);

            var expectedTrue = supportedCpuLine.Equals(supportedCpuLine2);

            expectedTrue.Should().BeFalse();
        }
    }
}