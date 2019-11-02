using Interactors.Providers;
using System;
using System.Linq;
using SimpleInjector;
using Interactors.Boundaries;
using Interactors.Repositories;
using System.Text;
using System.Collections.Generic;
using Entities;
using Interactors;

namespace BlackJackConsoleApp
{
	public class Program
	{
		private static readonly Container container = new Container();

		public static void Main(string[] args)
		{
			FillContainer();
			Console.WriteLine("Welcome to Casino Black Jack 101!");
			string gameIdentifier = string.Empty;
			string playerIdentifier = string.Empty;
			List<string> avitarIdentifiers = new List<string>();
			// these loops are set up for one player 
			// this will not nessasarly work for more than 4 players.
			for (int i = 0; i < BlackJackConstants.MaxPlayerCount; i++)
			{
				Console.WriteLine("Please enter your name.");
				var playerName = Console.ReadLine();
				var creatAvitarResponse = GetResponse<CreateAvitar.RequestModel, CreateAvitar.ResponseModel>(new CreateAvitar.RequestModel()
				{
					PlayerName = playerName
				});
				avitarIdentifiers.Add(creatAvitarResponse.Identifier);
			}

			foreach(string id in avitarIdentifiers) { 
				var joinGameResponse = GetResponse<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>(new JoinGameInteractor.RequestModel()
				{
					PlayerId = id
				});
				gameIdentifier = joinGameResponse.GameIdentifier;
				playerIdentifier = joinGameResponse.PlayerIdentifier;
			}
			// assumes that the gameId came back with good response and not multiple different id's
			var beginGameResponse = GetResponse<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>(new BeginGameInteractor.RequestModel()
			{
				Identifier = gameIdentifier,
				Dealer = new BlackJackPlayer("The_House_Always_Wins", new Player("Data"))
			});

			var gameStatus = beginGameResponse.Outcome;
			var game = beginGameResponse.Game;
			var currentPlayer = game.CurrentPlayer;
			var dealer = game.Players.Last();
			while(gameStatus != GameStatus.Complete)
			{
				ConsoleKey key;
				var validKeys = ActionKeys(currentPlayer.Hand.Actions.ToList());
				do
				{		
					Console.Clear();
					Console.WriteLine(" Dealer's Visible Cards.");
					Console.Write(VisibleCards(dealer.Hand, false));
					Console.WriteLine("---------------------------------------------");
					Console.WriteLine($" Player: {currentPlayer.Name}");
					Console.Write(VisibleCards(currentPlayer.Hand, true));
					Console.WriteLine(ActionMenuBuilder(currentPlayer.Hand.Actions.ToList()));
					
					key = Console.ReadKey(true).Key;
				} while (InValidActionKey(validKeys, key));
				
				switch (key)
				{
					case ConsoleKey.D:
						var holdGameResponse = GetResponse<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>(new HoldGameInteractor.RequestModel()
						{
							Identifier = gameIdentifier
						});
						game = holdGameResponse.Game;
						currentPlayer = game.CurrentPlayer;
						gameStatus = game.Status;
						break;
					case ConsoleKey.W:
						var hitGameResponse = GetResponse<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>(new HitGameInteractor.RequestModel()
						{
							Identifier = gameIdentifier
						});
						game = hitGameResponse.Game;
						currentPlayer = game.CurrentPlayer;
						gameStatus = game.Status;
						break;
					case ConsoleKey.S:
						Console.WriteLine("Split Function Not Supported Currently");						
						break;
					case ConsoleKey.Enter:
						Console.WriteLine("Player Loses, Continuing to Next Player.");
						holdGameResponse = GetResponse<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>(new HoldGameInteractor.RequestModel()
						{
							Identifier = gameIdentifier
						});
						game = holdGameResponse.Game;
						currentPlayer = game.CurrentPlayer;
						gameStatus = game.Status;
						break;
					default:
						throw new NotSupportedException();
				}
			} 
		}

		private static string ActionMenuBuilder(List<HandActionTypes> actions)
		{
			string actionKeys = string.Empty;
			for(int i = 0; i < actions.Count; i++)
			{
				var action = actions[i];
				actionKeys += "\t";
				switch(action)
				{
					case HandActionTypes.Hold:
						actionKeys += "D = Hold";
						break;
					case HandActionTypes.Draw:
						actionKeys += "W = Draw";
						break;
					case HandActionTypes.Split:
						actionKeys += "T = Split";
						break;
					case HandActionTypes.Pass:
						actionKeys += "Enter - Continue";
						break;
					default:
						throw new NotSupportedException();
				}
				if(i < actions.Count() - 1)
				{
					actionKeys += "\t";
				}
			}
			return actionKeys;
		}

		private static string VisibleCards(Hand hand, bool showAll)
		{
			string output = $" {hand.PointValue}\t";
			foreach(var card in hand.Cards)
			{
				if (showAll || !card.FaceDown)
				{
					output += $"[{card.Rank.ToString()} {card.Suit.ToString()}]";
					if (hand.Cards.Count() > 1)
					{
						output += card.Equals(hand.Cards.Last()) ? "\n" : ", ";
					}
				}
			}
			return output;
		}

		private static IEnumerable<ConsoleKey> ActionKeys(List<HandActionTypes> actions)
		{
			List<ConsoleKey> validKeys = new List<ConsoleKey>();
			actions.ForEach(a =>
			{
				switch (a)
				{
					case HandActionTypes.Hold:
						validKeys.Add(ConsoleKey.D);
						break;
					case HandActionTypes.Draw:
						validKeys.Add(ConsoleKey.W);
						break;
					case HandActionTypes.Split:
						validKeys.Add(ConsoleKey.S);
						break;
					case HandActionTypes.Pass:
						validKeys.Add(ConsoleKey.Enter);
						break;
					default:
						throw new NotSupportedException();
				}
			});
			return validKeys;
		}

		private static bool InValidActionKey(IEnumerable<ConsoleKey> actions, ConsoleKey key)
		{
			return !actions.Any(a => a.Equals(key));
		}

		private static void FillContainer()
		{
			container.RegisterSingleton<IPlayerRepository, InMemoryPlayerRepository>();
			container.RegisterSingleton<IGameRepository, InMemoryGameRepository>();
			container.RegisterSingleton<IGameIdentifierProvider, GuidBasedGameIdentifierProvider>();
			container.RegisterSingleton<ICardDeckProvider, CardDeckProvider>();
			container.RegisterSingleton<IPlayerIdentifierProvider, GuidBasedPlayerIdentifierProvider>();
			container.RegisterSingleton<IAvitarIdentifierProvider, GuidBasedAvitarIdentifierProvider>();
			container.Register(typeof(IOutputBoundary<>), typeof(IOutputBoundary<>).Assembly);
			container.Register(typeof(IInputBoundary<,>), typeof(IInputBoundary<,>).Assembly);
			//container.RegisterInitializer<InMemoryGameRepository>();

			container.Verify();
		}

		private static TResponseModel GetResponse<TRequestModel, TResponseModel>(TRequestModel requestModel)
		{
			var interactor = container.GetInstance<IInputBoundary<TRequestModel, TResponseModel>>();
			var presenter = new Presenter<TResponseModel>();
			interactor.HandleRequestAsync(requestModel, presenter);
			return presenter.ResponseModel;
		}
	}
}
