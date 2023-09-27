namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Reset password request
    /// </summary>
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
