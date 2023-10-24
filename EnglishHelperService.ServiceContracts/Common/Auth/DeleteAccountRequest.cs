namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Delete account request
    /// </summary>
    public class DeleteAccountRequest
    {
        /// <summary>
        /// It could be email or username
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
