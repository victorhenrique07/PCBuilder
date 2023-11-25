using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.MotherboardAndMemory;
using PCBuilder.Domain.Products.Shared;
using PCBuilder.Domain.Products.Cpus;

namespace PCBuilder.Tests
{
    public class MotherboardChipsetTests
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

        static CpuSocket cpuSocket = new CpuSocket("AM4", manufacturer);
        static MotherboardChipset motherboardChipset = new MotherboardChipset(
            name: "Z490",
            manufacturer: manufacturer,
            cpuSocket: cpuSocket,
            maxMemoryChannels: 4,
            supportsOverclocking: true,
            defaultBiosValueFactor: 10.0m,
            warrantyPeriod: 10.0m
        );


        [Fact]
        public void TestIfMotherBoardIsEqualTo()
        {
            MotherboardChipset motherboardChipset2 = new MotherboardChipset(
                name: "Z490",
                manufacturer: manufacturer,
                cpuSocket: cpuSocket,
                maxMemoryChannels: 4,
                supportsOverclocking: true,
                defaultBiosValueFactor: 10.0m,
                warrantyPeriod: 10.0m
            );

            var expectedTrue = motherboardChipset.Equals(motherboardChipset2);

            expectedTrue.Should().BeTrue();
        }

        [Fact]
        public void TestIfMotherBoardIsNotEqualsTo()
        {
            MotherboardChipset motherboardChipset2 = new MotherboardChipset(
            name: "B450",
            manufacturer: manufacturer,
            cpuSocket: cpuSocket,
            maxMemoryChannels: 4,
            supportsOverclocking: true,
            defaultBiosValueFactor: 10.0m,
            warrantyPeriod: 10.0m
        );

            var expectedFalse = motherboardChipset.Equals(motherboardChipset2);
            
            expectedFalse.Should().BeFalse();
        }

        [Fact]
        public void TestIfCpuIsSupported()
        {
            CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture(
                codename: "Tiger Lake",
                manufacturer: manufacturer,
                manufacturingProcess: "10nm"
            );

            CpuLine cpuLine = new CpuLine("Ryzen 5 5600", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);

            motherboardChipset.AddSupportedCpuLine(cpuLine, false);

            var expectedTrue = motherboardChipset.Supports(cpuLine, out bool needsBiosUpdate);

            expectedTrue.Should().BeTrue();
        }

        [Fact]
        public void TestIfCpuIsNotSupported()
        {
            CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture(
                codename: "Tiger Lake",
                manufacturer: manufacturer,
                manufacturingProcess: "10nm"
            );

            CpuLine cpuLine = new CpuLine("Ryzen 5 5600", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);
            CpuLine cpuLine2 = new CpuLine("Ryzen 7 5800X3D", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);

            motherboardChipset.AddSupportedCpuLine(cpuLine, false);

            var expectedFalse = motherboardChipset.Supports(cpuLine2, out bool needsBiosUpdate);

            expectedFalse.Should().BeFalse();
        }

        [Fact]
        public void TestIfAddSupportedCpuLineIsThrowingArgumentNullException()
        {
            Action expected = () => motherboardChipset.AddSupportedCpuLine(null, false);

            expected.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TestIfIsMotherboardChipSetControllerIsReturningNullCpuSocket()
        {
            Action motherboardChipset2 = () => new MotherboardChipset(
                name: "A320M",
                manufacturer: manufacturer,
                cpuSocket: cpuSocket,
                maxMemoryChannels: 4,
                supportsOverclocking: true,
                defaultBiosValueFactor: 10.0m,
                warrantyPeriod: 10.0m
            );

            motherboardChipset2.Should().Throw<ArgumentNullException>();
        }
    }
}