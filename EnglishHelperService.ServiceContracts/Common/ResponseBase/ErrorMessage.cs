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
        NoElementToModify,
        NoFilesUploaded,
        UploadedFileEmpty,
        InvalidFileFormat,
        UploadedFileFailed,
        InvalidFileData,
        SendEmailFailed,



        InvalidPasswordOrUsernameOrEmail,
        NoUsernameOrEmailExists,
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
        InvalidResetToken,

        EnglishTextRequired,
        HungarianTextRequired,
        EnglishWordExists,
        HungarianWordExists
    }
}