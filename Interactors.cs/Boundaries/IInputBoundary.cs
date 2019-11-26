namespace Interactors.Boundaries
{
	public interface IInputBoundary<TRequestModel, TResponseModel>
	{
		void HandleRequestAsync(TRequestModel requestModel, IOutputBoundary<TResponseModel> outputBoundary);
	}
}
