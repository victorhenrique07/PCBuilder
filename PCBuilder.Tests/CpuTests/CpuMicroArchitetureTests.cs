using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Cpus;
using PCBuilder.Domain.Products.Shared;

namespace PCBuilder.Tests
{
    public class CpuMicroarchitectureTests
    {
        static Manufacturer manufacturer = new Manufacturer("string name",
                                                    "string website",
                                                    "string twitterProfile",
                                                    "string facebookProfile",
                                                    "string instagramProfile", 
                                                    10.0m, 10.0m, 10.0m, 10.0m, 10.0m);

        static CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture("cpu1", manufacturer, "manufacturingProcess");

        [Fact]
        public void TestIfIsEqualsTo()
        {
            CpuMicroarchitecture cpuMicroarchitecture2 = new CpuMicroarchitecture("cpu1", manufacturer, "manufacturingProcess");

            Boolean expected = cpuMicroarchitecture.Equals(cpuMicroarchitecture2);

            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfIsNotEqualsTo()
        {
            CpuMicroarchitecture cpuMicroarchitecture2 = new CpuMicroarchitecture("cpu2", manufacturer, "manufacturingProcess");

            Boolean expected = cpuMicroarchitecture.Equals(cpuMicroarchitecture2);

            expected.Should().BeFalse();
        }

        [Theory]
        [InlineData(" ", "manufacturingProcess")]
        [InlineData(null, "manufacturingProcess")]
        [InlineData("cpu", " ")]
        [InlineData("cpu", null)]
        public void TestIfIsArgumentNullException(string invalidCodename, string invalidManufacturingProcess)
        {
            Action cpuMicroArchitecture = () => new CpuMicroarchitecture(invalidCodename, manufacturer, invalidManufacturingProcess);

            cpuMicroArchitecture.Should().Throw<ArgumentNullException>();

            Action cpuMicroArchitecture2 = () => new CpuMicroarchitecture(invalidCodename, null, invalidManufacturingProcess);

            cpuMicroArchitecture2.Should().Throw<ArgumentNullException>();
        }
    }
}