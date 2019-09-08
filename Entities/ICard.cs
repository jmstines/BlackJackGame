namespace Entities
{
    public interface ICard
    {
        Suit Suit { get; }
        string Display { get; }
        string Description { get; }
    }
}