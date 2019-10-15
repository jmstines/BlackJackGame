using Interactors.Providers;
using System;
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
			string gameId = string.Empty;
			List<string> playerIdentifiers = new List<string>();
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
				playerIdentifiers.Add(creatAvitarResponse.Identifier);
			}

			foreach(string  playerId in playerIdentifiers) { 
				var joinGameResponse = GetResponse<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>(new JoinGameInteractor.RequestModel()
				{
					PlayerId = playerId
				});
				gameId = joinGameResponse.Identifier;
			}
			// assumes that the gameId came back with good response and not multiple different id's
			var startGameResponse = GetResponse<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>(new BeginGameInteractor.RequestModel()
			{
				Identifier = gameId,
				Dealer = new BlackJackPlayer("The_House_Always_Wins", new Player("Data"))
			});

			var gameStatus = startGameResponse.Outcome;
			var game = startGameResponse.Game;
			var currentPlayer = game.CurrentPlayer;
			while(gameStatus != GameStatus.Complete)
			{
				ConsoleKey key;
				do
				{
					Console.Clear();
					Console.WriteLine($"Player: {currentPlayer.Name}");
					Console.WriteLine("\t S = Spin \t\t G = Guess");
					key = Console.ReadKey(true).Key;
				} while (InValidActionKey(key));
			} 
		}

		private static string ActionBuilder(IEnumerable<HandActionTypes> actions)
		{
			string actionKeys = string.Empty;

		}

		private static bool InValidActionKey(ConsoleKey key)
		{
			return key != ConsoleKey.T && key != ConsoleKey.D;
		}

		private static void FillContainer()
		{
			container.RegisterSingleton<IPlayerRepository, InMemoryPlayerRepository>();
			container.RegisterSingleton<IGameRepository, InMemoryGameRepository>();
			container.RegisterSingleton<IIdentifierProvider, GuidBasedIdentifierProvider>();
			//container.Register(typeof(IOutputBoundary<>), typeof(IOutputBoundary<>).Assembly);
			//container.Register(typeof(IInputBoundary<,>), typeof(IInputBoundary<,>).Assembly);
			//container.RegisterInitializer<InMemoryGameRepository>();

			//container.Verify();
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
