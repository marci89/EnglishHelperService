namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Login user request
    /// </summary>
    public class LoginUserRequest
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
