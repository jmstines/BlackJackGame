using Entities;
using Entities.Enums;
using Entities.Interfaces;
using Interactors;
using Interactors.Boundaries;
using Interactors.Providers;
using Interactors.Repositories;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJackConsoleApp
{
	static public class Program
	{
		private static readonly Container container = new Container();

		public static void Main(string[] args)
		{
			FillContainer();
			Console.WriteLine("Welcome to Casino Black Jack 101!");
			string gameIdentifier = string.Empty;
			string playerIdentifier = string.Empty; ;
			int maxPlayers = 1;
			List<string> avitarIdentifiers = new List<string>();

			for (int i = 0; i < maxPlayers; i++)
			{
				Console.WriteLine("Please enter your name.");
				var playerName = Console.ReadLine();
				var creatAvitarResponse = GetResponse<CreateAvitar.RequestModel, CreateAvitar.ResponseModel>(new CreateAvitar.RequestModel()
				{
					PlayerName = playerName
				});
				avitarIdentifiers.Add(creatAvitarResponse.AvitarIdentifier);
			}

			foreach (string id in avitarIdentifiers)
			{
				var joinGameResponse = GetResponse<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>(new JoinGameInteractor.RequestModel()
				{
					PlayerId = id,
					MaxPlayers = maxPlayers
				});
				gameIdentifier = joinGameResponse.GameIdentifier;
			}

			var beginGameResponse = GetResponse<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>(new BeginGameInteractor.RequestModel()
			{
				GameIdentifier = gameIdentifier,
				PlayerIdentifier = playerIdentifier
			});

			var gameStatus = beginGameResponse.Game.Status;
			var game = beginGameResponse.Game;
			var currentPlayer = game.Players.Where(p => p.PlayerIdentifier == game.CurrentPlayerId).Single();
			var dealer = game.Players.Where(p => p.PlayerIdentifier == game.DealerId).Single();
			while (gameStatus == GameStatus.InProgress)
			{
				ConsoleKey key;
				var validKeys = ActionKeys(currentPlayer.Hands.First().Value.Actions.ToList());
				do
				{
					Console.Clear();
					Console.WriteLine(" Dealer's Visible Cards.");
					//Console.Write($" \t{VisibleCards(dealer.Hands.First().Value, false)}");
					Console.WriteLine("---------------------------------------------");
					Console.WriteLine($" Player: {currentPlayer.Name}");
					//Console.Write($" {currentPlayer.Hands.First().Value.PointValue}\t{VisibleCards(currentPlayer.Hands.First().Value, true)}");
					Console.WriteLine(ActionMenuBuilder(currentPlayer.Hands.First().Value.Actions.ToList()));

					key = Console.ReadKey(true).Key;
				} while (InValidActionKey(validKeys, key));

				switch (key)
				{
					case ConsoleKey.D:
						var holdGameResponse = GetResponse<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>(new HoldGameInteractor.RequestModel()
						{
							GameIdentifier = gameIdentifier,
							PlayerIdentifier = playerIdentifier
						});
						game = holdGameResponse.Game;
						currentPlayer = game.Players.Where(p => p.PlayerIdentifier == game.CurrentPlayerId).Single();
						gameStatus = game.Status;
						break;
					case ConsoleKey.W:
						var hitGameResponse = GetResponse<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>(new HitGameInteractor.RequestModel()
						{
							GameIdentifier = gameIdentifier,
							PlayerIdentifier = playerIdentifier
						});
						game = hitGameResponse.Game;
						currentPlayer = game.Players.Where(p => p.PlayerIdentifier == game.CurrentPlayerId).Single();
						gameStatus = game.Status;
						break;
					case ConsoleKey.S:
						Console.WriteLine("Split Function Not Supported Currently");
						Console.ReadKey();
						break;
					case ConsoleKey.Enter:
						Console.WriteLine("Player Loses, Continuing to Next Player.");
						Console.ReadKey();
						holdGameResponse = GetResponse<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>(new HoldGameInteractor.RequestModel()
						{
							GameIdentifier = gameIdentifier,
							PlayerIdentifier = playerIdentifier
						});
						game = holdGameResponse.Game;
						currentPlayer = game.Players.Where(p => p.PlayerIdentifier == game.CurrentPlayerId).Single();
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
			for (int i = 0; i < actions.Count; i++)
			{
				var action = actions[i];
				actionKeys += "\t";
				switch (action)
				{
					case HandActionTypes.Hold:
						actionKeys += "D = Hold";
						break;
					case HandActionTypes.Draw:
						actionKeys += "W = Draw";
						break;
					case HandActionTypes.Split:
						actionKeys += "S = Split";
						break;
					case HandActionTypes.Pass:
						actionKeys += "Enter - Continue";
						break;
					default:
						throw new NotSupportedException();
				}
				if (i < actions.Count() - 1)
				{
					actionKeys += "\t";
				}
			}
			return actionKeys;
		}

		//private static string VisibleCards(HandDto hand, bool showAll)
		//{
		//	string output = string.Empty;
		//	foreach (var card in hand.Cards)
		//	{
		//		if (showAll || !card.FaceDown)
		//		{
		//			output += $"[{card.Rank.ToString()} {card.Suit.ToString()}]";
		//			if (hand.Cards.Count() > 1)
		//			{
		//				output += card.Equals(hand.Cards.Last()) ? "\n" : ", ";
		//			}
		//		}
		//	}
		//	return output;
		//}

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
			container.RegisterSingleton<IDealerProvider, DealerProvider>();
			container.RegisterSingleton<IRandomProvider, RandomProvider>();
			container.RegisterSingleton<ICardProvider, CardProvider>();
			container.RegisterSingleton<Deck, Deck>();
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
