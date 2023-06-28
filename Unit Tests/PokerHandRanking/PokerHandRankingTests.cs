using NUnit.Framework;
using static Unit_Tests.PokerHandRanking.PokerHandRanking;

namespace Unit_Tests.PokerHandRanking
{
    internal class PokerHandRankingTests
    {
        [Test]
        public void IsValidSuit_ShouldReturnTrue_ForValidSuits()
        {
            // Arrange
            char[] validSuits = { 'S', 'H', 'D', 'C' };

            // Act & Assert
            foreach (var suit in validSuits)
            {
                Assert.IsTrue(PokerHandRanker.IsValidSuit(suit));
            }
        }

        [Test]
        public void IsValidSuit_ShouldReturnFalse_ForInvalidSuit()
        {
            // Arrange
            char invalidSuit = 'X';

            // Act
            bool result = PokerHandRanker.IsValidSuit(invalidSuit);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidValue_ShouldReturnTrue_ForValidValues()
        {
            // Arrange
            string[] validValues = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };

            // Act & Assert
            foreach (var value in validValues)
            {
                Assert.IsTrue(PokerHandRanker.IsValidValue(value));
            }
        }

        [Test]
        public void IsValidValue_ShouldReturnFalse_ForInvalidValue()
        {
            // Arrange
            string invalidValue = "13";

            // Act
            bool result = PokerHandRanker.IsValidValue(invalidValue);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetHandRank_ShouldReturn_HighCard()
        {
            // Arrange
            var pokerHand = new PokerHandRanking.PokerHand();
            pokerHand.Cards.Add(new PokerHandRanking.Card { Suit = 'S', Value = "2" });
            pokerHand.Cards.Add(new PokerHandRanking.Card { Suit = 'H', Value = "4" });
            pokerHand.Cards.Add(new PokerHandRanking.Card { Suit = 'D', Value = "6" });
            pokerHand.Cards.Add(new PokerHandRanking.Card { Suit = 'C', Value = "8" });
            pokerHand.Cards.Add(new PokerHandRanking.Card { Suit = 'S', Value = "K" });

            // Act
            var result = pokerHand.GetHandRank();

            // Assert
            Assert.AreEqual(PokerHandRanking.HandRank.HighCard, result);
        }

        [Test]
        public void CompareHands_ShouldReturn_BlackWins_HighCard()
        {
            // Arrange
            var blackHand = "2S 4H 6D 8C KS";
            var whiteHand = "3S 5H 7D 9C TS";

            // Act
            var result = PokerHandRanking.PokerHandRanker.CompareHands(blackHand, whiteHand);

            // Assert
            Assert.AreEqual("Black wins - high card: 13", result);
        }

        [Test]
        public void CompareHands_ShouldReturn_WhiteWins_Flush()
        {
            // Arrange
            var blackHand = "2S 4S 6S 8S KS";
            var whiteHand = "3D 5D 7D 9D TD";

            // Act
            var result = PokerHandRanking.PokerHandRanker.CompareHands(blackHand, whiteHand);

            // Assert
            Assert.AreEqual("Black wins - high card: 13" , result);
        }
    }
}
