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
			Console.WriteLine("Please enter your name.");
			var playerName = Console.ReadLine();
			var creatAvitarResponse = GetResponse<CreateAvitar.RequestModel, CreateAvitar.ResponseModel>(new CreateAvitar.RequestModel() 
			{
				PlayerName = playerName
			});
			var startNewGameResponse = GetResponse<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>(new JoinGameInteractor.RequestModel()
			{
				PlayerId = creatAvitarResponse.Identifier
			});

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
