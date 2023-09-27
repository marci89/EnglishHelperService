namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// forgot password response
    /// </summary>
    public class ForgotPasswordResponse : ResponseBase
    {
        public ResetPasswordData Result { get; set; }
    }
}
