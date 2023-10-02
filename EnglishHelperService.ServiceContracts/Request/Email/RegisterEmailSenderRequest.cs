namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Register email sender request object
    /// </summary>
    public class RegisterEmailSenderRequest : SendEmailRequestBase
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
    }
}
