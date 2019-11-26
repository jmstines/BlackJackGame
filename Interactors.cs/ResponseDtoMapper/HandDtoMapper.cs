﻿using Entities;
using Interactors.ResponceDtos;
using System;
using System.Linq;


namespace Interactors.ResponseDtoMapper
{
	public class HandDtoMapper
	{
		private readonly Hand Hand;
		public HandDtoMapper(Hand hand)
		{
			Hand = hand ?? throw new ArgumentNullException(nameof(hand));
		}

		public HandDto Map(bool showAll)
		{
			var dto = new HandDto();
			if (showAll)
			{
				dto.Actions = Hand.Actions;
				dto.Cards = Hand.Cards;
				dto.PointValue = Hand.PointValue;
				dto.Status = Hand.Status;
			}
			else
			{
				dto.Actions = Hand.Actions;
				dto.Cards = Hand.Cards.Where(c => c.FaceDown.Equals(false));
				dto.PointValue = HandValue.GetValue(dto.Cards);
				dto.Status = Hand.Status;
			}
			return dto;
		}
	}
}
