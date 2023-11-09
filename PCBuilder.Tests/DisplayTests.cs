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

            var displayResolution1 = new DisplayResolution("TV1", 1920, 1080);
            var panelType = PanelType.OLED;
            var display1 = new Display(15.6m, displayResolution1, panelType, 60);

            var displayResolution2 = new DisplayResolution("TV2", 1920, 1080);
            var display2 = new Display(20.5m, displayResolution2, panelType, 60);

            Assert.False(display1.Equals(display2));
        }

        [Fact]
        public void TestIfDisplay1IsEqualToDisplay2()
        {

            var displayResolution1 = new DisplayResolution("TV1", 1920, 1080);
            var panelType = PanelType.OLED;
            var display1 = new Display(15.6m, displayResolution1, panelType, 60);

            var displayResolution2 = new DisplayResolution("TV1", 1920, 1080);
            var display2 = new Display(15.6m, displayResolution2, panelType, 60);

            Assert.True(display1.Equals(display2));
        }
    }
}