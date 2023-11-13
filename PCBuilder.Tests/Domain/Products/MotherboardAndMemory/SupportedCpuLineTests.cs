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
            "string name",
            "string website",
            "string twitterProfile",
            "string facebookProfile",
            "string instagramProfile", 
            10.0m, 10.0m, 10.0m, 10.0m, 10.0m);

        static CpuMicroarchitecture cpuMicroarchitecture = new CpuMicroarchitecture("Architecture", manufacturer, "weqweq");

        static CpuSocket cpuSocket = new CpuSocket("AM4", manufacturer);
        static CpuLine cpuLine = new CpuLine("R5", cpuMicroarchitecture, cpuSocket, 10.0m, 10.0m);
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
            SupportedCpuLine supportedCpuLine2 = new SupportedCpuLine(new CpuLine("R7", cpuMicroarchitecture, cpuSocket, 10.1m, 11.1m), false);

            var expectedTrue = supportedCpuLine.Equals(supportedCpuLine2);

            expectedTrue.Should().BeFalse();
        }
    }
}