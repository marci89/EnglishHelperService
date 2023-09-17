
using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.API.Helpers
{
    /// <summary>
    /// Logging error messages class
    /// </summary>
    public class ErrorLogger
    {
        private readonly ILogger<ErrorLogger> _logger;

        public ErrorLogger(ILogger<ErrorLogger> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logging error
        /// </summary>
        public void LogError(LoggerRequest request)
        {
            string errorMessage = GetErrorMessage(request.Response);
            string logMessage = $" Username: {request.Username}, UserId: {request.UserId}," +
                $" Controller: {request.ControllerName}, Action: {request.ControllerAction}," +
                $" Request: {request.Request}, Error: {errorMessage}";

            _logger.LogError(logMessage);
        }

        // Helper method to extract error message from the response or provide a default
        private string GetErrorMessage(ResponseBase response)
        {
            if (response != null)
            {
                if (!string.IsNullOrWhiteSpace(response.ExceptionErrorMessage))
                {
                    return response.ExceptionErrorMessage;
                }

                if (response.ErrorMessage != null)
                {
                    return response.ErrorMessage.ToString();
                }
            }

            // If response is null or empty, return a default message
            return "Response is null or empty";
        }
    }
}