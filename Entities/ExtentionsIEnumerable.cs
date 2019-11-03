using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class ExtentionsIEnumerable
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
		{
			var source = new List<T>(list);
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

		public static T RandomItem<T>(this IEnumerable<T> list) {
			var Random = new Random((int)DateTime.UtcNow.Ticks);

			var nextIndex = Random.Next(minValue: 0, maxValue: list.Count());
			return list.ElementAt(nextIndex);
		}
	}
}
