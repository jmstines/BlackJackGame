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
			while(gameStatus != GameStatus.Complete)
			{
				ConsoleKey key;
				var validKeys = ActionKeys(currentPlayer.Hand.Actions.ToList());
				do
				{
					Console.Clear();
					Console.WriteLine($"Player: {currentPlayer.Name}");
					Console.WriteLine(ActionMenuBuilder(currentPlayer.Hand.Actions.ToList()));
					key = Console.ReadKey(true).Key;
				} while (IsValidActionKey(validKeys, key));

				switch (key)
				{
					case ConsoleKey.D:
						var holdGameResponse = GetResponse<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>(new HoldGameInteractor.RequestModel()
						{
							Identifier = gameIdentifier
						});
						game = holdGameResponse.Game;
						currentPlayer = holdGameResponse.Game.CurrentPlayer;
						gameStatus = holdGameResponse.Game.Status;
						break;
					case ConsoleKey.W:
						var hitGameResponse = GetResponse<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>(new HitGameInteractor.RequestModel()
						{
							Identifier = gameIdentifier
						});
						game = hitGameResponse.Game;
						currentPlayer = hitGameResponse.Game.CurrentPlayer;
						gameStatus = hitGameResponse.Game.Status;
						break;
					case ConsoleKey.S:
						Console.WriteLine("Split Function Not Supported Currently");						
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
					default:
						throw new NotSupportedException();
				}
			});
			return validKeys;
		}

		private static bool IsValidActionKey(IEnumerable<ConsoleKey> actions, ConsoleKey key)
		{
			return actions.Any(a => a.Equals(key));
		}

		private static void FillContainer()
		{
			container.RegisterSingleton<IPlayerRepository, InMemoryPlayerRepository>();
			container.RegisterSingleton<IGameRepository, InMemoryGameRepository>();
			container.RegisterSingleton<IGameIdentifierProvider, GuidBasedGameIdentifierProvider>();
			container.RegisterSingleton<IPlayerIdentifierProvider, GuidBasedPlayerIdentifierProvider>();
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
