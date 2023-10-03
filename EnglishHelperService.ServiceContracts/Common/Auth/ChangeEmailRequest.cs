namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// User email change request by logined user id
    /// </summary>
    public class ChangeEmailRequest
    {
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