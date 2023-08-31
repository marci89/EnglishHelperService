using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	public class BaseValidator<TResponse> where TResponse : ResponseBase, new()
	{
		public TResponse CreateErrorResponse(ErrorMessage errorMessage, StatusCode statusCode = StatusCode.BadRequest)
		{
			return new TResponse
			{
				StatusCode = statusCode,
				ErrorMessage = errorMessage
			};
		}
	}
}
