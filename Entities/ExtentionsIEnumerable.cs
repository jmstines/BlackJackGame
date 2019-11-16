using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class ExtentionsIEnumerable
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list, IRandomProvider randomProvider)
		{
			var source = list?.ToList() ?? throw new ArgumentNullException(nameof(list));
			_ = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
			
			var shuffled = new List<T>();
			while (source.Any())
			{
				var nextIndex = randomProvider.GetRandom(min: 0, max: source.Count);
				var currentItem = source.ElementAt(nextIndex);
				source.Remove(currentItem);
				shuffled.Add(currentItem);
			}
			return shuffled;
		}
	}
}
