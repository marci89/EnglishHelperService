namespace EnglishHelperService.Persistence.Entities
{
    /// <summary>
    /// User entity
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
        public RoleType Role { get; set; }

        /// <summary>
        /// User's hashed password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User created date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// User last activity date
        /// </summary>
        public DateTime LastActive { get; set; }

        /// <summary>
        /// Reset token for reseting password
        /// </summary>
        public string ResetToken { get; set; }

        /// <summary>
        /// Reset token expiration time for reseting password
        /// </summary>
        public DateTime? ResetTokenExpiration { get; set; }


        /// <summary>
        /// User's words
        /// </summary>
        public ICollection<Word> Words { get; set; }

    }
}

