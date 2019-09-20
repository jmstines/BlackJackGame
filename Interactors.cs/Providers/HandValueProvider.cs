using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
    public static class HandValueProvider
    {
        public static int GetValue(List<BlackJackCard> cards)
        {
            _ = cards != null ? false : throw new ArgumentNullException(nameof(cards));
            int value = GetHandValue(cards);
            var aceCount = GetAceCount(cards);
            for(int i = 0; i < aceCount; i++)
            {
                ReduceAceValueIfBustHand(value);
            }
            return value;
        }

        private static int GetHandValue(List<BlackJackCard> cards) => 
            cards.Sum(c => GetCardValue(c.Rank));

        private static int GetAceCount(List<BlackJackCard> cards) => 
            cards.Count(c => c.Rank.Equals(CardRank.Ace));

        private static int ReduceAceValueIfBustHand(int value) => 
            value > BlackJackGameConstants.BlackJack ? value : value - 10;

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
