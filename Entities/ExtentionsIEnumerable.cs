using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class ExtentionsIEnumerable
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
		{
			var source = list.ToList() ?? throw new ArgumentNullException(nameof(list));
			var Random = new Random((int)DateTime.UtcNow.Ticks);
			var shuffled = new List<T>();
			while (source.Any())
			{
				var nextIndex = Random.Next(minValue: 0, maxValue: source.Count);
				var currentItem = source.ElementAt(nextIndex);
				source.Remove(currentItem);
				shuffled.Add(currentItem);
			}
			return shuffled;
		}

		
	}
}
