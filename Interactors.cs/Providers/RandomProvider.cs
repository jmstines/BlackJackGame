using System;

namespace Interactors.Providers
{
    public class RandomProvider : Random, IRandomProvider
    {
        public RandomProvider() { }
        public RandomProvider(int Seed) : base(Seed) { }
    }
}