namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// User object for client
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User Role
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// User created date
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// User last activity date
        /// </summary>
        public DateTime LastActive { get; set; }
    }
}
