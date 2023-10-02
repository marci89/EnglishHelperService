namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Update user request
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
    }
}
