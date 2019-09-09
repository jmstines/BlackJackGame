namespace Interactors.Providers
{
    public interface IRandomProvider
    {
        int Next(int minValue, int maxValue);
    }
}