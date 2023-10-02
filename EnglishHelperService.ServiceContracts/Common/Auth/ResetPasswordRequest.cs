namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Reset password request
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password reset link token
        /// </summary>
        public string Token { get; set; }
    }
}
