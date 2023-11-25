using System;
using Xunit;
using FluentAssertions;
using PCBuilder.Domain.Products.Peripherals;

namespace PCBuilder.Tests
{
    public class DisplayTests
    {
        static DisplayResolution displayResolution = new DisplayResolution(name: "Full HD", columns: 1920, rows: 1080);

        static Display display1 = new Display(
            size: 27.0m,
            resolution: displayResolution,
            panelType: PanelType.LCD,
            refreshRate: 144
        );

        [Fact]
        public void TestIfDisplay1IsNotEqualToDisplay2()
        {
            var display2 = new Display(20.5m, displayResolution, PanelType.OLED, 60);
            
            bool areEqual = display1.Equals(display2);

            areEqual.Should().BeFalse();
        }

        [Fact]
        public void TestIfDisplay1IsEqualToDisplay2()
        {
        
            var display2 = new Display(15.6m, displayResolution, PanelType.OLED, 60);

            // Act
            bool areEqual = display1.Equals(display2);

            // Assert
            areEqual.Should().BeTrue();
        }

        [Fact]
        public void TestIfPixelsPerInchIsMatching()
        {

            // Arrange
            var display = new Display(15.6m, displayResolution, PanelType.OLED, 60);

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
            var display = new Display(15.6m, displayResolution, PanelType.OLED, 60);

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
            var display = new Display(15.6m, displayResolution, PanelType.OLED, 60);
        
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
            var display = new Display(15.6m, displayResolution, PanelType.OLED, 60);

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
            var display = new Display(15.6m, displayResolution, PanelType.OLED, 60);

            // Act
            var expectedArea = display.Height * display.Width;

            // Assert
            display.Area.Should().Be(expectedArea);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void TestIfDisplaySizeIsThrowingExceptionArgumentOutOfRangeException(decimal invalidSize)
        {
            Action display = () => new Display(invalidSize, displayResolution, PanelType.LCD, 60);

            display.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void TestIfResolutionIsThrowingExceptionArgumentNullException()
        {
            Action display = () => new Display(15m, null, PanelType.OLED, 60);

            display.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TestIfPanelTypeIsThrowingExceptionArgumentOutOfRangeException()
        {
            Action display = () => new Display(15m, displayResolution, (PanelType)999, 60);

            display.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}