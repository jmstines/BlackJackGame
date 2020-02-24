using Entities;
using Interactors.ResponceDtos;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Interactors.ResponseDtoMapper
{
	public class HandDtoMapper
	{
		private readonly IDictionary<string, Hand> Hands;
		public HandDtoMapper(IDictionary<string, Hand> hands) => 
			Hands = hands ?? throw new ArgumentNullException(nameof(hands));

		public IDictionary<string, HandDto> Map(bool showAll)
		{
			IDictionary<string, HandDto> handDtos = new Dictionary<string, HandDto>();
			foreach (var hand in Hands) {
				var dto = new HandDto();
				var handValues = hand.Value;
				if (showAll)
				{
					dto.Actions = handValues.Actions;
					dto.Cards = handValues.Cards;
					dto.PointValue = handValues.PointValue;
					dto.Status = handValues.Status;
				}
				else
				{
					dto.Actions = handValues.Actions;
					dto.Cards = handValues.Cards.Where(c => c.FaceDown.Equals(false));
					dto.PointValue = handValues.PointValue;
					dto.Status = handValues.Status;
				}
				handDtos.Add(hand.Key, dto);
			}
			return handDtos;
		}
	}
}
