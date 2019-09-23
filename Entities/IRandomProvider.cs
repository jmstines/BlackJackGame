namespace Entities
    public interface IRandomProvider
    {
        int Next(int minValue, int maxValue);
    }
}