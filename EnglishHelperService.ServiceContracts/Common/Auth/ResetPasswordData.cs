namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Reset password datas for email template
    /// </summary>
    public class ResetPasswordData
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
