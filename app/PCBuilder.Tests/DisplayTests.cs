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
            
            Assert.NotNull(display.Resolution);
            Assert.Equal(display.PixelsPerInch, expectedPixelsPerInch);
        }

        [Fact]
        public void TestIfAspectRatioIsMatching()
        {
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            Assert.NotNull(display.Resolution);
            Assert.Equal((decimal)16/9, display.AspectRatio);
        }

        [Fact]
        public void TestIfHeightIsMatching()
        {
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);
            
            var expectecHeight = display.Size / (decimal)Math.Sqrt(Math.Pow((double)display.AspectRatio, 2.0) + 1);

            Assert.NotNull(display.Resolution);
            Assert.Equal(expectecHeight, display.Height);
        }

        [Fact]
        public void TestIfWidthIsMatching()
        {
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);
        }

        [Fact]
        public void TestIfAreaIsMatching()
        {
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);
        }
    }
}