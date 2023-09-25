namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Register email sender request object
    /// </summary>
    public class RegisterEmailSenderRequest : SendEmailRequestBase
    {
        public string Username { get; set; }
    }
}
