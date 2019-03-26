using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
  [Serializable]
  public class Game
  {
    private List<Card> Deck { get; set; }
    public List<Player> Players { get; private set; }
    public Player Dealer { get; private set; }
    public bool GameComplete { get; private set; }

    private const int BlackJack = 21;
    private const int DealerHoldValue = 17;

    public Game(List<Card> deck, List<Player> players)
    {
      Deck = deck ?? throw new ArgumentNullException(nameof(deck));      
      Players = players ?? throw new ArgumentNullException(nameof(players));
      if (Players.Count > 4)
      {
        throw new ArgumentOutOfRangeException(nameof(players), "Player Count must be less than 5 Players.");
      }
      else if(Players.Count < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(players), "Must have at least one Player.");
      }

      Dealer = new Player("Dealer");

      GameComplete = false;
      DealHands();
    }

    public void PlayerDrawsCard(Player player)
    {
      Card card = Deck.FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(Deck), "Card Deck is Empty.");
      Deck.Remove(card);
      player.DrawCard(card);

      if (Dealer.Hand.Count < 1)
      {
        CalculateOutcome();
      }
    }

    private void DealHands()
    {
      foreach (Player player in Players)
      {
        PlayerDrawsCard(player);
        PlayerDrawsCard(player);
      }
      PlayerDrawsCard(Dealer);
      PlayerDrawsCard(Dealer);
    }

    private void CalculateOutcome()
    {
      if(HasBlackjack(Dealer))
      {
        Players.ForEach(p => p.Status = HasBlackjack(p) ? Outcome.Push : Outcome.PlayerLoses);

        Dealer.Status = Players.Any(w => w.Status == Outcome.PlayerWins) ? Outcome.PlayerLoses : Outcome.PlayerWins;
        GameComplete = true;
      }
      else if(BustHand(Dealer))
      {
        Players.ForEach(p => p.Status = BustHand(p) ? Outcome.PlayerLoses : Outcome.PlayerWins);

        Dealer.Status = Players.Any(w => w.Status == Outcome.PlayerWins) ? Outcome.PlayerLoses : Outcome.PlayerWins;
        GameComplete = true;
      }
      //if (!Player.CurentHand.Any() || !Dealer.CurentHand.Any())
      //{
      //  //Outcome = Outcome.Undecided;
      //  //return;
      //}
      //throw new NotImplementedException();
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

    private bool DealerMustDraw() => Dealer.PointTotal < DealerHoldValue;
    private bool HasBlackjack(Player player) => player.PointTotal == BlackJack;
    private bool BustHand(Player player) => player.PointTotal > BlackJack;
  }
}
