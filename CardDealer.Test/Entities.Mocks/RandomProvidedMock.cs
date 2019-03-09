using CardDealer;
using System;
using System.Linq;

namespace Tests.Mocks
{
  class RandomProviderMock : IRandomProvider
  {
    private readonly int[] Seed;
    private readonly int CardCount;
    private int CurrentIndex = 0;

    public RandomProviderMock(int cardCount)
    {
      if (cardCount < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(cardCount));
      }
      CardCount = cardCount;
      Seed = Enumerable.Range(0, cardCount).Reverse().ToArray();
    }

    public RandomProviderMock(int[] seed)
    {
      Seed = seed ?? throw new ArgumentNullException(nameof(seed));
      CardCount = Seed.Count();
    }

    public int Next(int minValue, int maxValue)
    {
      if (CurrentIndex >= CardCount) throw new ArgumentOutOfRangeException(nameof(CurrentIndex));
      int currentValue = Seed[CurrentIndex];
      CurrentIndex++;
      return currentValue;
    }
  }
}
