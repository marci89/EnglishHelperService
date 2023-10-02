namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// User email change request
    /// </summary>
    public class ChangeEmailRequest
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}