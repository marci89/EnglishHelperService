namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Login user object for client
    /// </summary>
    public class LoginUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
