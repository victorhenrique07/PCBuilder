using System;
using Xunit;
using PCBuilder.Domain.Products.Peripherals;

namespace PCBuilder.Tests
{
    public class DisplayExceptions
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void TestIfDisplaySizeIsThrowingExceptionArgumentOutOfRangeException(decimal invalidSize)
        {

            Action display = () => new Display(invalidSize, new DisplayResolution("TV", 1920, 1080), PanelType.LCD, 60);

            Assert.Throws<ArgumentOutOfRangeException>(display);

        }

        [Fact]
        public void TestIfResolutionIsThrowingExceptionArgumentNullException()
        {
            Action display = () => new Display(15m, null, PanelType.OLED, 60);

            Assert.Throws<ArgumentNullException>(display);

        }

        [Fact]
        public void TestIfPanelTypeIsThrowingExceptionArgumentOutOfRangeException()
        {
            Action display = () => new Display(15m, new DisplayResolution("TV", 1920, 1080), (PanelType)999, 60);

            Assert.Throws<ArgumentOutOfRangeException>(display);
        }
    }
}