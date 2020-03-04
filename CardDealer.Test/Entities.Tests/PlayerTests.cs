using Entities.Enums;
using NUnit.Framework;
using System;

namespace Entities.Tests
{
	public class PlayerTests
	{
		private const string playerName = "Sam";

		[Test]
		public void NewPlayer_NullName_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Avitar(null));
		}

		[Test]
		public void NewPlayer_Sam_CorrectName()
		{
			var sam = new Avitar(playerName);
			Assert.AreEqual(playerName, sam.Name);
		}
	}
}
