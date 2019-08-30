using System;

namespace Entities
{
  public class RandomProvider : Random, IRandomProvider
  {
    public RandomProvider() { }
    public RandomProvider(int Seed) : base(Seed) { }
  }
}