using Interactors.Boundaries;

namespace BlackJackConsoleApp
{
	internal class Presenter<TResponseModel> : IOutputBoundary<TResponseModel>
	{
		internal TResponseModel ResponseModel;

		public void HandleResponse(TResponseModel responseModel) => ResponseModel = responseModel;
	}
}
