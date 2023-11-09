using System;
using Xunit;
using PCBuilder.Domain.Products.Peripherals;

namespace PCBuilder.Tests
{
    public class DisplayTests
    {
        [Fact]
        public void TestIfDisplay1IsNotEqualToDisplay2()
        {

            var display1 = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            var display2 = new Display(20.5m, new DisplayResolution("TV2", 1920, 1080), PanelType.OLED, 60);
            
            Assert.False(display1.Equals(display2));
        }

        [Fact]
        public void TestIfDisplay1IsEqualToDisplay2()
        {
            var display1 = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            var display2 = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            Assert.True(display1.Equals(display2));
        }

        [Fact]
        public void TestIfPixelsPerInchIsMatching()
        {

            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            var expectedPixelsPerInch = display.Resolution.Rows / display.Height;

            Assert.Equal(display.PixelsPerInch, expectedPixelsPerInch);
        }

        [Fact]
        public void TestIfAspectRatioIsMatching()
        {
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            var expectedAspectRatio = (decimal) display.Resolution.Columns / display.Resolution.Rows;

            Assert.Equal(display.AspectRatio, expectedAspectRatio);
        }
    }
}