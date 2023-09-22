namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Import word list from text file request
    /// </summary>
    public class ImportWordListFromTextFileRequest
    {
        public long UserId { get; set; }
        public string Content { get; set; }
    }
}
