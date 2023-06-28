using NUnit.Framework;

namespace Unit_Tests.StringCalculator
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("");

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Add_SingleNumber_ReturnsNumber()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("1");

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("1,2");

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Add_UnknownAmountOfNumbers_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("1,2,3,4,5");

            // Assert
            Assert.AreEqual(15, result);
        }

        [Test]
        public void Add_NewLinesBetweenNumbers_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("1\n2,3");

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_DifferentDelimiter_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add(";\n1;2;3");

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_NegativeNumber_ThrowsException()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Assert
            Assert.Throws<Exception>(() => calculator.Add("-1"));
        }

        [Test]
        public void Add_MultipleNegativeNumbers_ThrowsExceptionWithAllNegatives()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Assert
            var exception = Assert.Throws<Exception>(() => calculator.Add("-1,2,-3,4,-5"));
            StringAssert.Contains("Negatives not allowed: -1, -3, -5", exception.Message);
        }

        [Test]
        public void Add_NumbersBiggerThan1000_AreIgnored()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("2,1001");

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Add_DelimitersOfAnyLength_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("//[***]\n1***2***3");

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_MultipleDelimiters_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("//[*][%]\n1*2%3");

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_MultipleDelimitersWithLengthLongerThanOneChar_ReturnsSum()
        {
            // Arrange
            var calculator = new StringCalculator();

            // Act
            int result = calculator.Add("//[**][%%]\n1**2%%3");

            // Assert
            Assert.AreEqual(6, result);
        }
    }
}