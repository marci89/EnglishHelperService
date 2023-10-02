namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Import word list from text file request
    /// </summary>
    public class ImportWordListFromTextFileRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The text file's content
        /// </summary>
        public string Content { get; set; }
    }
}
