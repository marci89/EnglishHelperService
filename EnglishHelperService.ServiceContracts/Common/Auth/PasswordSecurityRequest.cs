namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Password Verify request
    /// </summary>
    public class PasswordSecurityRequest
    {
        /// <summary>
        /// Hashed password
        /// </summary>
        public string HashedPassword { get; set; }
        /// <summary>
        /// Original password (not hashed password from client)
        /// </summary>
        public string Password { get; set; }
    }
}
