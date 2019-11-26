using Entities.Enums;
using NUnit.Framework;

namespace Entities.Tests
{
	class BlackJackCardValueTests
	{
		[Test]
		public void Ace_GetValue_ValueEquals_11()
		{
			Assert.AreEqual(11, BlackJackCardValue.GetValue(CardRank.Ace));
		}

		[Test]
		public void Two_GetValue_ValueEquals_2()
		{
			Assert.AreEqual(2, BlackJackCardValue.GetValue(CardRank.Two));
		}

		[Test]
		public void Three_GetValue_ValueEquals_3()
		{
			Assert.AreEqual(3, BlackJackCardValue.GetValue(CardRank.Three));
		}

		[Test]
		public void Four_GetValue_ValueEquals_4()
		{
			Assert.AreEqual(4, BlackJackCardValue.GetValue(CardRank.Four));
		}

		[Test]
		public void Five_GetValue_ValueEquals_5()
		{
			Assert.AreEqual(5, BlackJackCardValue.GetValue(CardRank.Five));
		}

		[Test]
		public void Six_GetValue_ValueEquals_6()
		{
			Assert.AreEqual(6, BlackJackCardValue.GetValue(CardRank.Six));
		}

		[Test]
		public void Seven_GetValue_ValueEquals_7()
		{
			Assert.AreEqual(7, BlackJackCardValue.GetValue(CardRank.Seven));
		}

		[Test]
		public void Eight_GetValue_ValueEquals_8()
		{
			Assert.AreEqual(8, BlackJackCardValue.GetValue(CardRank.Eight));
		}

		[Test]
		public void Nine_GetValue_ValueEquals_9()
		{
			Assert.AreEqual(9, BlackJackCardValue.GetValue(CardRank.Nine));
		}

		[Test]
		public void Ten_GetValue_ValueEquals_10()
		{
			Assert.AreEqual(10, BlackJackCardValue.GetValue(CardRank.Ten));
		}

		[Test]
		public void Queen_GetValue_ValueEquals_10()
		{
			Assert.AreEqual(10, BlackJackCardValue.GetValue(CardRank.Queen));
		}

		[Test]
		public void King_GetValue_ValueEquals_10()
		{
			Assert.AreEqual(10, BlackJackCardValue.GetValue(CardRank.King));
		}
	}
}
