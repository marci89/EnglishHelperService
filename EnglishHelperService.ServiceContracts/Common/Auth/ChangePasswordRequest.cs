namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// User password change request
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// New password
        /// </summary>
        public string NewPassword { get; set; }
    }
}
