using Entities;
using System;

namespace Interactors
{
    [Serializable]
    public class BlackJackGame
    {
        public readonly CardGame Game;

        public BlackJackGame(CardGame game)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            BlackJackGameActions.DealHands(game);
            BlackJackOutcomes.CalculateOutcome(game);
        }

        public void PlayerAction(PlayerAction action)
        {
            switch (action)
            {
                case Entities.PlayerAction.Draw:
                    BlackJackGameActions.PlayerDrawsCard(Game);
                    break;
                case Entities.PlayerAction.Hold:
                    BlackJackGameActions.PlayerHolds(Game);
                    break;
                case Entities.PlayerAction.Split:
                    throw new NotImplementedException();
                    //break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
            BlackJackOutcomes.BustHandCheck(Game);
        }
    }
}
