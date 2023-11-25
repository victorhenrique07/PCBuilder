using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Shared;

namespace PCBuilder.Tests
{
    public class CpuMicroarchitectureTests
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

        [Fact]
        public void TestIfIsEqualsTo()
        {
            CpuMicroarchitecture cpuMicroarchitecture2 = new CpuMicroarchitecture(
            codename: "Tiger Lake",
            manufacturer: manufacturer,
            manufacturingProcess: "10nm"
        );

            Boolean expected = cpuMicroarchitecture.Equals(cpuMicroarchitecture2);

            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfIsNotEqualsTo()
        {
            CpuMicroarchitecture cpuMicroarchitecture2 = new new CpuMicroarchitecture(
                codename: "Vermeer",
                manufacturer: manufacturer,
                manufacturingProcess: "10nm"
            );

            Boolean expected = cpuMicroarchitecture.Equals(cpuMicroarchitecture2);

            expected.Should().BeFalse();
        }

        [Theory]
        [InlineData(" ", "11nm")]
        [InlineData(null, "11nm")]
        [InlineData("Vermeer", " ")]
        [InlineData("Vermeer", null)]
        public void TestIfIsArgumentNullException(string invalidCodename, string invalidManufacturingProcess)
        {
            Action cpuMicroArchitecture = () => new CpuMicroarchitecture(invalidCodename, manufacturer, invalidManufacturingProcess);

            cpuMicroArchitecture.Should().Throw<ArgumentNullException>();

            Action cpuMicroArchitecture2 = () => new CpuMicroarchitecture(invalidCodename, null, invalidManufacturingProcess);

            cpuMicroArchitecture2.Should().Throw<ArgumentNullException>();
        }
    }
}