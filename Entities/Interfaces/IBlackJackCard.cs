namespace Entities.Interfaces
{
	public interface IBlackJackCard : ICard
	{
		bool FaceDown { get; }
		int Value { get; }
	}
}
