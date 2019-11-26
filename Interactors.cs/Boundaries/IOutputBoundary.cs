namespace Interactors.Boundaries
{
	public interface IOutputBoundary<TResponseModel>
	{
		void HandleResponse(TResponseModel responseModel);
	}
}
