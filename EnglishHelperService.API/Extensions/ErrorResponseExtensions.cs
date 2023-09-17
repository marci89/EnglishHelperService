using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EnglishHelperService.API.Extensions
{
    public static class ErrorResponseExtensions
    {
        /// <summary>
        /// Create error response for controller actions
        /// </summary>
        public static IActionResult CreateErrorResponse(this ControllerBase controller, ResponseBase response)
        {
            if (response is null)
            {
                return controller.BadRequest("Invalid response object");
            }

            switch (response.StatusCode)
            {
                case StatusCode.BadRequest:
                    return controller.BadRequest(response.ErrorMessage.ToString());
                case StatusCode.Unauthorized:
                    return controller.Unauthorized(response.ErrorMessage.ToString());
                case StatusCode.NotFound:
                    return controller.NotFound(response.ErrorMessage.ToString());
                case StatusCode.InternalServerError:
                    return controller.StatusCode((int)HttpStatusCode.InternalServerError, response.ErrorMessage.ToString());
                default:
                    return controller.BadRequest(response.ErrorMessage.ToString());
            }
        }
    }
}