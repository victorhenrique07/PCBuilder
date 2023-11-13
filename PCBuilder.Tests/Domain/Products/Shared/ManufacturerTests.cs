using System;
using Xunit;
using FluentAssertions;

namespace PCBuilder.Domain.Products.Shared
{
    public class ManufacturerTests
    {

        static Manufacturer manufacturer = new Manufacturer("string name",
                                                    "string website",
                                                    "string twitterProfile",
                                                    "string facebookProfile",
                                                    "string instagramProfile", 
                                                    10.0m, 10.0m, 10.0m, 10.0m, 10.0m);


        [Fact]
        public void TestIfManufacturerIsEqualsTo()
        {
            Manufacturer manufacturer2 = new Manufacturer("string name",
                                                    "string website",
                                                    "string twitterProfile",
                                                    "string facebookProfile",
                                                    "string instagramProfile", 
                                                    10.0m, 10.0m, 10.0m, 10.0m, 10.0m);

            var expectedTrue = manufacturer.Equals(manufacturer2);

            expectedTrue.Should().BeTrue();
        }

        [Fact]
        public void TestIfManufacturerIsNotEqualsTo()
        {
            Manufacturer manufacturer2 = new Manufacturer("name",
                                                    "website",
                                                    "twitterProfile",
                                                    "facebookProfile",
                                                    "instagramProfile", 
                                                    11.0m, 11.0m, 11.0m, 11.0m, 11.0m);

            var expectedFalse = manufacturer.Equals(manufacturer);

            expectedFalse.Should().BeTrue();
        }

        [Fact]
        public void TestIfCpuWarrantyQualityModifierIsReturning0m()
        {
            var expected = manufacturer.CpuWarrantyQualityModifier;

            expected.Should().Be(0m);
        }

        [Fact]
        public void TestIfMonitorWarrantyQualityModifierIsReturning0m()
        {
            var expected = manufacturer.MonitorWarrantyQualityModifier;

            expected.Should().Be(0m);
        }
    }
}