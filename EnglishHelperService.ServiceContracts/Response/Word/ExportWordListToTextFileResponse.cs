namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Export word list to txt file response
    /// </summary>
    public class ExportWordListToTextFileResponse : ResponseBase
    {
        public MemoryStream Result { get; set; }
    }
}