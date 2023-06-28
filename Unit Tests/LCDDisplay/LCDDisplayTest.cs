using NUnit.Framework;

namespace Unit_Tests.LCDDisplay
{
    internal class LCDDisplayTest
    {
        [TestFixture]
        public class LCDDisplayTests
        {
            [Test]
            public void GenerateLCDDisplay_123_ReturnsCorrectDisplay()
            {
                // Arrange
                var lcdDisplay = new LCDDisplay();
                int number = 123;
                string expectedDisplay =
                    "... ._. ._. \n" +
                    "..| ._| ._| \n" +
                    "..| |_. ._| ";

                // Act
                string display = lcdDisplay.GenerateLCDDisplay(number);

                // Assert
                Assert.AreEqual(expectedDisplay, display);
            }

            [Test]
            public void GenerateLCDDisplay_456_ReturnsCorrectDisplay()
            {
                // Arrange
                var lcdDisplay = new LCDDisplay();
                int number = 456;
                string expectedDisplay =
                    "... ._. ._. \n" +
                    "|_| |_. |_. \n" +
                    "..| ._| |_| ";

                // Act
                string display = lcdDisplay.GenerateLCDDisplay(number);

                // Assert
                Assert.AreEqual(expectedDisplay, display);
                Console.WriteLine(display); // Вывод на консоль
            }
            [Test]
            public void GenerateLCDDisplay_0_ReturnsCorrectDisplay()
            {
                // Arrange
                var lcdDisplay = new LCDDisplay();
                int number = 0;
                string expectedDisplay =
                    "._. \n" +
                    "|.| \n" +
                    "|_| ";

                // Act
                string display = lcdDisplay.GenerateLCDDisplay(number);

                // Assert
                Assert.AreEqual(expectedDisplay, display);
            }

            [Test]
            public void GenerateLCDDisplay_98765_ReturnsCorrectDisplay()
            {
                // Arrange
                var lcdDisplay = new LCDDisplay();
                int number = 98765;
                string expectedDisplay =
                    "._. ._. ._. ._. ._. \n" +
                    "|_| |_| ..| |_. |_. \n" +
                    "..| |_| ..| |_| ._| ";

                // Act
                string display = lcdDisplay.GenerateLCDDisplay(number);

                // Assert
                Assert.AreEqual(expectedDisplay, display);
            }

            [Test]
            public void GenerateLCDDisplay_1234567890_ReturnsCorrectDisplay()
            {
                // Arrange
                var lcdDisplay = new LCDDisplay();
                int number = 1234567890;
                string expectedDisplay =
                    "... ._. ._. ... ._. ._. ._. ._. ._. ._. \n" +
                    "..| ._| ._| |_| |_. |_. ..| |_| |_| |.| \n" +
                    "..| |_. ._| ..| ._| |_| ..| |_| ..| |_| ";

                // Act
                string display = lcdDisplay.GenerateLCDDisplay(number);

                // Assert
                Assert.AreEqual(expectedDisplay, display);
            }
        }
    }
}
