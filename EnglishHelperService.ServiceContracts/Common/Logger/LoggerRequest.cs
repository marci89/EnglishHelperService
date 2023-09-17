namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Custom logger request object
    /// </summary>
    public class LoggerRequest
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string ControllerName { get; set; }
        public string ControllerAction { get; set; }
        public ResponseBase Response { get; set; }
        public string Request { get; set; }
    }
}
