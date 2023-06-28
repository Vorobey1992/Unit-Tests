

namespace Unit_Tests.PokerHandRanking
{
    internal class PokerHandRanking
    {
        public enum HandRank
        {
            HighCard,
            Pair,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        public enum ValueRank
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public class Card
        {
            public char Suit { get; set; }
            public string Value { get; set; } = string.Empty;
        }

        public class PokerHand
        {
            public List<Card> Cards { get; set; } = new List<Card>();

            public HandRank GetHandRank()
            {
                if (IsRoyalFlush()) return HandRank.RoyalFlush;
                if (IsStraightFlush()) return HandRank.StraightFlush;
                if (IsFourOfAKind()) return HandRank.FourOfAKind;
                if (IsFullHouse()) return HandRank.FullHouse;
                if (IsFlush()) return HandRank.Flush;
                if (IsStraight()) return HandRank.Straight;
                if (IsThreeOfAKind()) return HandRank.ThreeOfAKind;
                if (IsTwoPairs()) return HandRank.TwoPairs;
                if (IsPair()) return HandRank.Pair;

                return HandRank.HighCard;
            }

            private bool IsFourOfAKind()
            {
                var groupedValues = Cards.GroupBy(c => c.Value);
                return groupedValues.Any(g => g.Count() == 4);
            }

            private bool IsFullHouse()
            {
                var groupedValues = Cards.GroupBy(c => c.Value);
                return groupedValues.Any(g => g.Count() == 3) && groupedValues.Any(g => g.Count() == 2);
            }

            private bool IsFlush()
            {
                return Cards.GroupBy(c => c.Suit).Count() == 1;
            }

            private bool IsStraight()
            {
                var orderedValues = Cards.Select(c => GetValueRank(c.Value)).OrderBy(r => r).ToList();
                var distinctValues = orderedValues.Distinct().ToList();

                // Check for Ace, 2, 3, 4, 5 straight
                if (distinctValues.Count == 5 && distinctValues.Max() == (int)ValueRank.Ace && distinctValues.Min() == (int)ValueRank.Two)
                    return true;

                // Check for other straights
                for (int i = 0; i < orderedValues.Count - 1; i++)
                {
                    if (orderedValues[i + 1] - orderedValues[i] != 1)
                        return false;
                }

                return true;
            }


            private bool IsStraightFlush()
            {
                return IsStraight() && IsFlush();
            }

            private bool IsRoyalFlush()
            {
                var orderedValues = Cards.Select(c => (int)GetValueRank(c.Value)).OrderBy(r => r).ToList();
                var distinctValues = orderedValues.Distinct().ToList();

                // Check for Ace, King, Queen, Jack, 10 straight
                return distinctValues.Count == 5 &&
                       distinctValues.Contains((int)ValueRank.Ace) &&
                       distinctValues.Contains((int)ValueRank.King) &&
                       distinctValues.Contains((int)ValueRank.Queen) &&
                       distinctValues.Contains((int)ValueRank.Jack) &&
                       distinctValues.Contains((int)ValueRank.Ten);
            }

            private bool IsThreeOfAKind()
            {
                var groupedValues = Cards.GroupBy(c => c.Value);
                return groupedValues.Any(g => g.Count() == 3);
            }

            private bool IsTwoPairs()
            {
                var groupedValues = Cards.GroupBy(c => c.Value);
                return groupedValues.Count(g => g.Count() == 2) == 2;
            }

            private bool IsPair()
            {
                var groupedValues = Cards.GroupBy(c => c.Value);
                return groupedValues.Any(g => g.Count() == 2);
            }

            public static int GetValueRank(string value)
            {
                if (Enum.TryParse<ValueRank>(value, out var rank))
                {
                    return (int)rank;
                }
                else
                {
                    // Обработка значений, не являющихся числами
                    return value.ToUpper() switch
                    {
                        "T" => (int)ValueRank.Ten,
                        "J" => (int)ValueRank.Jack,
                        "Q" => (int)ValueRank.Queen,
                        "K" => (int)ValueRank.King,
                        "A" => (int)ValueRank.Ace,
                        _ => throw new ArgumentException("Invalid card value: " + value),
                    };
                }
            }

        }

        public class PokerHandRanker
        {
            public static string CompareHands(string blackHand, string whiteHand)
            {
                var blackCards = ParseHand(blackHand);
                var whiteCards = ParseHand(whiteHand);

                var blackPokerHand = new PokerHand { Cards = blackCards };
                var whitePokerHand = new PokerHand { Cards = whiteCards };

                var blackRank = blackPokerHand.GetHandRank();
                var whiteRank = whitePokerHand.GetHandRank();

                if (blackRank > whiteRank)
                    return "Black wins - " + blackRank.ToString();
                if (blackRank < whiteRank)
                    return "White wins - " + whiteRank.ToString();

                var blackHighestCardValue = blackCards.Max(c => PokerHand.GetValueRank(c.Value));
                var whiteHighestCardValue = whiteCards.Max(c => PokerHand.GetValueRank(c.Value));

                if (blackHighestCardValue > whiteHighestCardValue)
                    return "Black wins - high card: " + blackHighestCardValue;
                if (blackHighestCardValue < whiteHighestCardValue)
                    return "White wins - high card: " + whiteHighestCardValue;

                return "Tie";
            }

            private static List<Card> ParseHand(string hand)
            {
                var cards = new List<Card>();

                var cardStrings = hand.Split(' ');
                foreach (var cardString in cardStrings)
                {
                    if (cardString.Length != 2)
                    {
                        throw new ArgumentException("Invalid card format: " + cardString);
                    }

                    var suit = cardString[1];
                    var value = cardString[0].ToString();

                    if (!IsValidSuit(suit))
                    {
                        throw new ArgumentException("Invalid card suit: " + suit);
                    }

                    if (!IsValidValue(value))
                    {
                        throw new ArgumentException("Invalid card value: " + value);
                    }

                    cards.Add(new Card { Suit = suit, Value = value.ToString() });
                }

                return cards;
            }

            public static bool IsValidSuit(char suit)
            {
                return suit == 'S' || suit == 'H' || suit == 'D' || suit == 'C';
            }

            public static bool IsValidValue(string value)
            {
                var validValues = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
                return validValues.Contains(value);
            }
        }
    }
}
