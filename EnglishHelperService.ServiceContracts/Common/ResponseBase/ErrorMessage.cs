namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Error messages for client
    /// </summary>
    public enum ErrorMessage
    {
        InvalidRequest,
        ServerError,
        NotFound,
        DeleteFailed,
        EditFailed,
        CreateFailed,

        InvalidPasswordOrUsernameOrEmail,
        UsernameOrEmailRequired,
        UsernameRequired,
        UsernameMaxLength,
        UsernameExists,
        PasswordRequired,
        InvalidPasswordFormat,
        InvalidOldPassword,
        EmailRequired,
        EmailExists,
        InvalidEmailFormat,

        EnglishTextRequired,
        HungarianTextRequired,
        EnglishWordExists,
        HungarianWordExists
    }
}