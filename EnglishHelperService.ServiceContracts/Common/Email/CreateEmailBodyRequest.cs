namespace EnglishHelperService.ServiceContracts
{

    /// <summary>
    /// Request for email body creating
    /// </summary>
    public class CreateEmailBodyRequest
    {
        /// <summary>
        /// Email template name without .html and language part
        /// </summary>
        public string EmailTemplateName { get; set; }
        /// <summary>
        /// Current language
        /// </summary>
        public string Language { get; set; }
    }
}