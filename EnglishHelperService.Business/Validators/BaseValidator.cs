using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
	/// <summary>
	/// Base validator for validators
	/// </summary>
	public class BaseValidator
	{
		/// <summary>
		/// Response base helper to return custom error
		/// </summary>
		public TResponse CreateErrorResponse<TResponse>(ErrorMessage errorMessage, StatusCode statusCode = StatusCode.BadRequest)
			where TResponse : ResponseBase, new()
		{
			return new TResponse
			{
				StatusCode = statusCode,
				ErrorMessage = errorMessage
			};
		}

		/// <summary>
		/// Create not found error
		/// </summary>
		public Task<TResponse> CreateNotFoundResponse<TResponse>()
			where TResponse : ResponseBase, new()
		{
			return Task.FromResult(new TResponse
			{
				StatusCode = StatusCode.NotFound,
				ErrorMessage = ErrorMessage.NotFound
			});
		}

		/// <summary>
		/// Create Creation error
		/// </summary>
		public Task<TResponse> CreateCreationErrorResponse<TResponse>()
			where TResponse : ResponseBase, new()
		{
			return Task.FromResult(new TResponse
			{
				StatusCode = StatusCode.InternalServerError,
				ErrorMessage = ErrorMessage.CreateFailed
			});
		}

		/// <summary>
		/// Create update error
		/// </summary>
		public Task<TResponse> CreateUpdateErrorResponse<TResponse>()
			where TResponse : ResponseBase, new()
		{
			return Task.FromResult(new TResponse
			{
				StatusCode = StatusCode.InternalServerError,
				ErrorMessage = ErrorMessage.EditFailed
			});
		}

		/// <summary>
		/// Create delete error
		/// </summary>
		public Task<TResponse> CreateDeleteErrorResponse<TResponse>()
			where TResponse : ResponseBase, new()
		{
			return Task.FromResult(new TResponse
			{
				StatusCode = StatusCode.InternalServerError,
				ErrorMessage = ErrorMessage.DeleteFailed
			});
		}

		/// <summary>
		/// Create server error
		/// </summary>
		public Task<TResponse> CreateServerErrorResponse<TResponse>()
			where TResponse : ResponseBase, new()
		{
			return Task.FromResult(new TResponse
			{
				StatusCode = StatusCode.InternalServerError,
				ErrorMessage = ErrorMessage.ServerError
			});
		}
	}
}
