namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// User password change request by logined user id
    /// </summary>
    public class ChangePasswordRequest
    {
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
