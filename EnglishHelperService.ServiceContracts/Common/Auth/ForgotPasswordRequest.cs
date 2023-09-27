namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// forgot password request
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// It could be email or username
        /// </summary>
        public string Identifier { get; set; }
    }
}
