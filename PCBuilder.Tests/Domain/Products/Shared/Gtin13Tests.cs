using System;
using FluentAssertions;
using Xunit;
using PCBuilder.Domain.Products.Shared;

namespace PCBuilder.Tests
{
    public class Gtin13Tests
    {

        static Gtin13 gtin13 = new Gtin13("12345678-1234");


        [Fact]
        public void TestIfParameterNumberIsValid()
        {
            Action gtin13 = () => new Gtin13("12345678-1234");

            gtin13.Should().NotThrow<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("12345678-123")]
        [InlineData("123456-1234")]
        [InlineData("12345678-12345")]
        [InlineData("1234567891-1234")]
        public void TestIfParameterNumberIsNotValid(string number)
        {
            Action gtin13 = () => new Gtin13(number);

            gtin13.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void TestIfGtin13IsEqualsTo()
        {
            Gtin13 gtin = new Gtin13("12345678-1234");

            var expectedTrue = gtin13.Equals(gtin);

            expectedTrue.Should().BeTrue();
        }

        [Fact]
        public void TestIfGtin13IsNotEqualsTo()
        {
            Gtin13 gtin = new Gtin13("1234567-1234");

            var expectedFalse = gtin13.Equals(gtin);

            expectedFalse.Should().BeFalse();
        }
    }
}