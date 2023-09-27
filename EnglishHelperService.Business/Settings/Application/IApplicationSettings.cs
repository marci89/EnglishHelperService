namespace EnglishHelperService.Business.Settings
{
    /// <summary>
    ///  IApplicationSettings interface
    /// </summary>
    public interface IApplicationSettings
    {
        /// <summary>
        /// Client domain url
        /// </summary>
        string ClientDomain { get; }

        /// <summary>
        /// Application name
        /// </summary>
        string ApplicationName { get; }
    }
}
