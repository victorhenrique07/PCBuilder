using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Shared;

namespace PCBuilder.Domain.Products.Shared
{

    public class ProductIdentificationTests
    {
        static Gtin13 gtin13 = new Gtin13("12345678-1234");
        static ProductIdentification productIdentification = new ProductIdentification("1", gtin13);

        [Fact]
        public void TestIfProductIdentificationIsEqualsTo()
        {
            ProductIdentification productIdentification2 = new ProductIdentification("1", gtin13);

            var expectedTrue = productIdentification.Equals(productIdentification2);

            expectedTrue.Should().BeTrue();
        }

        [Fact]
        public void TestIfProductIdentificationIsNotEqualsTo()
        {
            ProductIdentification productIdentification2 = new ProductIdentification("2", gtin13);

            var expectedFalse = productIdentification.Equals(productIdentification2);

            expectedFalse.Should().BeFalse();
        }

        [Fact]
        public void TestIfIsThrowingArgumentNullException()
        {
            Action productIdentification2 = () => new ProductIdentification(null, null);

            productIdentification2.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TestIfIsThrowingArgumentOutOfRangeException()
        {
            Action productIdentification2 = () => new ProductIdentification(" ", gtin13);

            productIdentification2.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}