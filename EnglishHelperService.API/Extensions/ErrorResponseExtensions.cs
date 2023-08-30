using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHelperService.API.Extensions
{
	public static class ErrorResponseExtensions
	{
		public static IActionResult CreateErrorResponse(this ControllerBase controller, ResponseBase response)
		{
			switch (response.StatusCode)
			{
				case StatusCode.BAD_REQUEST:
					return controller.BadRequest(response.ErrorMessage.ToString());
				case StatusCode.UNAUTHORIZED:
					return controller.Unauthorized(response.ErrorMessage.ToString());
				case StatusCode.NOT_FOUND:
					return controller.NotFound(response.ErrorMessage.ToString());
				case StatusCode.INTERNAL_SERVER_ERROR:

					return controller.StatusCode(500, response.ErrorMessage.ToString());
				default:
					return controller.BadRequest(response.ErrorMessage.ToString());
			}
		}
	}
}