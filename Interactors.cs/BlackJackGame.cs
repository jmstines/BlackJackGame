using Entities;
using System;
using System.Linq;

namespace Interactors
{
    [Serializable]
    public class BlackJackGame
    {
        public readonly CardGame Game;
        public bool GameComplete { get; private set; }

        public BlackJackGame(CardGame game)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            GameComplete = false;
            BlackJackDealHands.DealHands(game);
            BlackJackCalculateOutcome.CalculateOutcome(game);
        }

        public void PlayerAction(PlayerAction action)
        {
            switch (action)
            {
                case Entities.PlayerAction.Draw:
                    BlackJackActionDrawCard.PlayerDrawsCard(Game);
                    break;
                case Entities.PlayerAction.Hold:
                    BlackJackActionHold.PlayerHolds(Game);
                    break;
                case Entities.PlayerAction.Split:
                    throw new NotImplementedException();
                    //break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
            BlackJackBustHandCheck.BustHandCheck(Game);
        }
    }
}
