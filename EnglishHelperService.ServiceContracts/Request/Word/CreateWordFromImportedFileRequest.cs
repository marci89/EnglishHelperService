namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Careate word request if it is an imported file
    /// </summary>
    public class CreateWordFromImportedFileRequest
    {
        public long UserId { get; set; }
        public string EnglishText { get; set; }
        public string HungarianText { get; set; }
    }
}
