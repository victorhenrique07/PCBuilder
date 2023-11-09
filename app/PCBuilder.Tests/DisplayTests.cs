using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Peripherals;

namespace PCBuilder.Tests
{
    public class DisplayTests
    {
        [Fact]
        public void TestIfDisplay1IsNotEqualToDisplay2()
        {
            // Arrange
            var display1 = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);
            var display2 = new Display(20.5m, new DisplayResolution("TV2", 1920, 1080), PanelType.OLED, 60);
            
            // Act
            bool areEqual = display1.Equals(display2);

            // Assert
            areEqual.Should().BeFalse();
        }

        [Fact]
        public void TestIfDisplay1IsEqualToDisplay2()
        {
            // Arrange
            var display1 = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);
            var display2 = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            // Act
            bool areEqual = display1.Equals(display2);

            // Assert
            areEqual.Should().BeTrue();
        }

        [Fact]
        public void TestIfPixelsPerInchIsMatching()
        {

            // Arrange
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            // Act
            var expectedPixelsPerInch = display.Resolution.Rows / display.Height;
            
            // Assert
            display.Resolution.Should().NotBeNull();
            display.PixelsPerInch.Should().Be(expectedPixelsPerInch);
        }

        [Fact]
        public void TestIfAspectRatioIsMatching()
        {
            // Arrange
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            // Act
            decimal expectedAspectRation = (decimal)16/9;


            // Assert
            display.Resolution.Should().NotBeNull();
            display.AspectRatio.Should().Be(expectedAspectRation);
        }

        [Fact]
        public void TestIfHeightIsMatching()
        {
            // Arrange
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);
        
            // Act
            var expectedHeight = display.Size / (decimal)Math.Sqrt(Math.Pow((double)display.AspectRatio, 2.0) + 1);

            // Assert
            display.Resolution.Should().NotBeNull();
            display.Height.Should().Be(expectedHeight);
        }

        [Fact]
        public void TestIfWidthIsMatching()
        {
            // Arrange
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            // Act
            var expectedWidth = display.AspectRatio * display.Height;

            // Assert
            display.Resolution.Should().NotBeNull();
            display.Width.Should().Be(expectedWidth);
        }

        [Fact]
        public void TestIfAreaIsMatching()
        {
            // Arrange
            var display = new Display(15.6m, new DisplayResolution("TV1", 1920, 1080), PanelType.OLED, 60);

            // Act
            var expectedArea = display.Height * display.Width;

            // Assert
            display.Area.Should().Be(expectedArea);
        }
    }
}