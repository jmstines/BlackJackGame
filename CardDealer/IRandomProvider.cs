using System;

namespace CardDealer
{
  public interface IRandomProvider
  {
    int Next(int minValue, int maxValue);
  }
}