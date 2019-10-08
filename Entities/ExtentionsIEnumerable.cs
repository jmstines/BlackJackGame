using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class ExtentionsIEnumerable
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
		{
			var source = new List<T>(list) ?? throw new ArgumentNullException(nameof(list));
			var Random = new Random((int)DateTime.UtcNow.Ticks);
			var shuffled = new List<T>();
			while (source.Any())
			{
				var nextIndex = Random.Next(minValue: 0, maxValue: source.Count);
				var currentItem = source.ElementAt(nextIndex);
				source.Remove(currentItem);
				shuffled.Add(currentItem);
			}
			return shuffled;
		}

		public static int Value(this IEnumerable<BlackJackCard> cards)
		{
			_ = cards ?? throw new ArgumentNullException(nameof(cards));
			int value = cards.Sum(c => GetCardValue(c.Rank));
			var aceCount = cards.Count(c => c.Rank.Equals(CardRank.Ace));
			for (int i = 0; i < aceCount; i++)
			{
				value =	value > BlackJackConstants.BlackJack ? value - 10 : value;
			}
			return value;
		}

		private static int GetCardValue(CardRank rank)
		{
			int value;
			switch (rank)
			{
				case CardRank.Ace:
					value = 11;
					break;
				case CardRank.Two:
					value = 2;
					break;
				case CardRank.Three:
					value = 3;
					break;
				case CardRank.Four:
					value = 4;
					break;
				case CardRank.Five:
					value = 5;
					break;
				case CardRank.Six:
					value = 6;
					break;
				case CardRank.Seven:
					value = 7;
					break;
				case CardRank.Eight:
					value = 8;
					break;
				case CardRank.Nine:
					value = 9;
					break;
				default:
					value = 10;
					break;
			}
			return value;
		}
	}
}
