using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
  [Serializable]
  public class Game
  {
    public List<Player> Players { get; private set; }
    public Player Dealer { get; private set; }
    public Outcome Outcome { get; private set; }

    public void DealCards(List<Player> players, Player dealer)
    {
      Players = players;
      Dealer = dealer;
      CalculateOutcome();
    }

    private void CalculateOutcome()
    {
      //if (!Player.CurentHand.Any() || !Dealer.CurentHand.Any())
      //{
      //  //Outcome = Outcome.Undecided;
      //  //return;
      //}
      throw new NotImplementedException();
      //if (Player.Value == Dealer.Value)
      //{
      //  Outcome = Outcome.Tie;
      //  return;
      //}
      //if (LeftBeatsRight(Player.Value, Dealer.Value))
      //  Outcome = Outcome.Player1Wins;
      //else
      //  Outcome = Outcome.Player2Wins;
    }
  }
}
