namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create user request
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
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
