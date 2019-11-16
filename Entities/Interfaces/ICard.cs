using Entities.Enums;

namespace Entities.Interfaces
{
	public interface ICard
	{
		CardSuit Suit { get; }
		CardRank Rank { get; }
	}
}
