﻿using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;

namespace Interactors
{
	public class HitGameInteractor : IInputBoundary<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string Identifier { get; set; }
		}

		public class ResponseModel
		{
			public GameStatus Outcome { get; set; }
			public BlackJackPlayer CurrentPlayer { get; set; }
		}

		private readonly IGameRepository GameRepository;

		public HitGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public async void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = await GameRepository.ReadAsync(requestModel.Identifier);

			game.PlayerHits();
			new BlackJackOutcomes(game).UpdateStatus();

			await GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Outcome = game.Status, CurrentPlayer = game.CurrentPlayer });
		}
	}
}
