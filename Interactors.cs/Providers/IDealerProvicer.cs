using Entities;

namespace Interactors.Providers
{
	public interface IDealerProvider
	{
		BlackJackPlayer Dealer { get; }
	}
}
