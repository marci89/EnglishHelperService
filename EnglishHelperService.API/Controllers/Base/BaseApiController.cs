using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHelperService.API.Controllers
{
    [Authorize]
    [ApiController]
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

        /// <summary>
        /// Validate uploaded files
        /// </summary>
        protected ResponseBase IsValidUploadedFiles(List<string> allowedExtensions)
        {
            var response = new ResponseBase();

            //Get files from request
            var files = Request.Form.Files;

            //Check null and count
            if (files == null || files.Count == 0)
                response.ErrorMessage = ErrorMessage.NoFilesUploaded;

            //Check file size
            var file = files[0];
            if (file.Length == 0)
                response.ErrorMessage = ErrorMessage.UploadedFileEmpty;

            //Check file extension validity
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                response.ErrorMessage = ErrorMessage.InvalidFileFormat;

            return response;
        }

        /// <summary>
        /// Get current language from Accept-Language header. The client set it where you need
        /// </summary>
        protected string GetCurrentLanguage()
        {
            // Get the 'Accept-Language' header from the request
            var acceptLanguageHeader = Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrWhiteSpace(acceptLanguageHeader))
                return "en";
            return acceptLanguageHeader;
        }
    }
}
