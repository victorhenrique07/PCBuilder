using System;
using FluentAssertions;
using Xunit;
using PCBuilder.Domain.Recommendations;

namespace PCBuilder.Tests
{
    public class UseProfileTests
    {
        static UseProfile useProfile = new UseProfile("name", 10m, 10m,
            10m, 10m,
            10m,
            10m, true);

        [Fact]
        public void TestIfUseProfileIsEqualsTo()
        {
            UseProfile useProfile2 = new UseProfile("name", 10m, 10m,
                10m, 10m,
                10m,
                10m, true);

            var expected = useProfile.Equals(useProfile2);

            expected.Should().BeTrue();
        }

        [Fact]
        public void TestIfUseProfileIsNotEqualsTo()
        {
            UseProfile useProfile2 = new UseProfile("name2", 10m, 10m,
                10m, 10m,
                10m,
                10m, true);

            var expected = useProfile.Equals(useProfile2);

            expected.Should().BeFalse();
        }

        [Fact]
        public void TestIfStogareDeviceSecondaryValueFactorForDoublePerformanceIsMatching()
        {
            var expected = 10m * 0.9m;

            expected.Should().Be(useProfile.StogareDeviceSecondaryValueFactorForDoublePerformance);
        }
    }
}