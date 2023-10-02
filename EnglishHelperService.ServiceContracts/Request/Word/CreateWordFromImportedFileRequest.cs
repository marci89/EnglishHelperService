namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Careate word request if it is an imported file
    /// </summary>
    public class CreateWordFromImportedFileRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// English text
        /// </summary>
        public string EnglishText { get; set; }
        /// <summary>
        /// Hungarian text
        /// </summary>
        public string HungarianText { get; set; }
    }
}
