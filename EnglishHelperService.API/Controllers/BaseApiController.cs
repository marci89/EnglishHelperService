using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHelperService.API.Controllers
{
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        private readonly ErrorLogger _logger;

        public BaseApiController(ErrorLogger logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Get logined user id from ClaimTypes
        /// </summary>
        protected long GetLoginedUserId()
        {
            return User.GetUserId();
        }

        /// <summary>
        /// Get logined user's name from ClaimTypes
        /// </summary>
        protected string GetLoginedUsername()
        {
            return User.GetUsername();
        }

        /// <summary>
        /// Logging Error
        /// </summary>
        protected void LogError(string request, ResponseBase response)
        {
            string controllerName = ControllerContext?.ActionDescriptor?.ControllerName;
            string controllerAction = ControllerContext?.ActionDescriptor?.ActionName;

            _logger.LogError(new LoggerRequest
            {
                UserId = GetLoginedUserId(),
                Username = GetLoginedUsername(),
                ControllerName = controllerName,
                ControllerAction = controllerAction,
                Request = request,
                Response = response
            });
        }
    }
}
