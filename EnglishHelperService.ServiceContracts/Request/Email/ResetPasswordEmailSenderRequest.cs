namespace EnglishHelperService.ServiceContracts
{

    /// <summary>
    /// Reset password email sender request object
    /// </summary>
    public class ResetPasswordEmailSenderRequest : SendEmailRequestBase
    {
        /// <summary>
        /// Current username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Client application name
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Link to set your new password
        /// </summary>
        public string ResetPasswordLink { get; set; }
    }
}