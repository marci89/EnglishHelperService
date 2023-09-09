using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHelperService.API.Extensions
{
	public static class ErrorResponseExtensions
	{
		/// <summary>
		/// Create error response for controller actions
		/// </summary>
		public static IActionResult CreateErrorResponse(this ControllerBase controller, ResponseBase response)
		{
			switch (response.StatusCode)
			{
				case StatusCode.BadRequest:
					return controller.BadRequest(response.ErrorMessage.ToString());
				case StatusCode.Unauthorized:
					return controller.Unauthorized(response.ErrorMessage.ToString());
				case StatusCode.NotFound:
					return controller.NotFound(response.ErrorMessage.ToString());
				case StatusCode.InternalServerError:
					return controller.StatusCode(500, response.ErrorMessage.ToString());
				default:
					return controller.BadRequest(response.ErrorMessage.ToString());
			}
		}
	}
}