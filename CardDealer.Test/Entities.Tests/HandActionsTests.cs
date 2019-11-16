using Entities.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class HandActionsTests
	{
		private readonly IEnumerable<HandActionTypes> DefualtActions = new List<HandActionTypes> { HandActionTypes.Draw, HandActionTypes.Hold };
		private readonly IEnumerable<HandActionTypes> SplitHandActions = new List<HandActionTypes> { HandActionTypes.Draw, HandActionTypes.Hold, HandActionTypes.Split };
		private readonly IEnumerable<HandActionTypes> EndHandActions = new List<HandActionTypes> { HandActionTypes.Hold };

		[Test]
		public void NewHand_DefaultValues_CorrectValues()
		{
			var deck = new Deck();
			var cards = new List<ICard>
			{
				deck.ElementAt(10),
				deck.ElementAt(1)
			};

			Assert.AreEqual(DefualtActions, HandActions.GetActions(cards));
		}

		[Test]
		public void NewHand_DefaultValues_IncorrectValues()
		{
			var deck = new Deck();
			var cards = new List<ICard>
			{
				deck.ElementAt(10),
				deck.ElementAt(1)
			};

			Assert.AreNotEqual(EndHandActions, HandActions.GetActions(cards));
		}

		[Test]
		public void NewHand2TenValues_SplitAndDefaultHand_CorrectValues()
		{
			var deck = new Deck();
			var cards = new List<ICard>
			{
				deck.ElementAt(10),
				deck.ElementAt(10)
			};

			Assert.AreNotEqual(SplitHandActions, HandActions.GetActions(cards));
		}

		[Test]
		public void BustHand_EndHandValues_CorrectValues()
		{
			var deck = new Deck();
			var cards = new List<ICard>
			{
				deck.ElementAt(10),
				deck.ElementAt(10),
				deck.ElementAt(10)
			};

			Assert.AreNotEqual(EndHandActions, HandActions.GetActions(cards));
		}
	}
}
