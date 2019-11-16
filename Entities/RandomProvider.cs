using Entities.Interfaces;
using System;

namespace Entities
{
	public class RandomProvider : IRandomProvider
	{
		private readonly Random Random;
		public RandomProvider() => Random = new Random((int)DateTime.UtcNow.Ticks);
		public int GetRandom(int min, int max) => Random.Next(min, max);
	}
}
