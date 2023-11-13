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
            "string name",
            "string website",
            "string twitterProfile",
            "string facebookProfile",
            "string instagramProfile", 
            10.0m, 10.0m, 10.0m, 10.0m, 10.0m);

        static CpuSocket cpuSocket = new CpuSocket("AM4", manufacturer);
        static MotherboardChipset motherboardChipset = new MotherboardChipset(
            "name",
            manufacturer,
            cpuSocket,
            123,
            true,
            10.0m,
            10.0m);


        [Fact]
        public void TestIfMotherBoardIsEqualTo()
        {
            MotherboardChipset motherboardChipset2 = new MotherboardChipset(
                "name",
                manufacturer,
                cpuSocket,
                123,
                true,
                10.0m,
                10.0m);

            var expectedTrue = motherboardChipset.Equals(motherboardChipset2);

            expectedTrue.Should().BeTrue();
        }

        [Fact]
        public void TestIfMotherBoardIsNotEqualsTo()
        {
            MotherboardChipset motherboardChipset2 = new MotherboardChipset(
                "name1",
                manufacturer,
                cpuSocket,
                1233,
                false,
                11.1m,
                11.1m);

            var expectedFalse = motherboardChipset.Equals(motherboardChipset2);
            
            expectedFalse.Should().BeFalse();
        }
        
    }
}