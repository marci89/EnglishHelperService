namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Email sender request object
    /// </summary>
    public class SendEmailRequestBase
    {
        /// <summary>
        /// Recipient email (to email)
        /// </summary>
        public string RecipientEmail { get; set; }
        /// <summary>
        /// Email's subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Email's body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Email language
        /// </summary>
        public string Language { get; set; }
    }
}
