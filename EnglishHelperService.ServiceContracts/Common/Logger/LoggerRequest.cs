namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Custom logger request object
    /// </summary>
    public class LoggerRequest
    {
        /// <summary>
        /// Logined user's id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Logined user's name
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Current controller name
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// Current controller action name
        /// </summary>
        public string ControllerAction { get; set; }
        /// <summary>
        /// ResponseBase object
        /// </summary>
        public ResponseBase Response { get; set; }
        /// <summary>
        /// Controller action request model
        /// </summary>
        public string Request { get; set; }
    }
}
