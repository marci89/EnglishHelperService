namespace EnglishHelperService.Business.Settings
{
    /// <summary>
    ///  ISecuritySettings interface
    /// </summary>
    public interface ISecuritySettings
    {
        /// <summary>
        /// Key for jwt token
        /// </summary>
        string TokenKey { get; }

        /// <summary>
        /// Jwt token expiration days
        /// </summary>
        int TokenExpirationDays { get; }
    }
}