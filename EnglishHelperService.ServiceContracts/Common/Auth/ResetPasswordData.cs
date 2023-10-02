namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Reset password datas for email template
    /// </summary>
    public class ResetPasswordData
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Reset password link token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
