namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Export word list to txt, excel file response
    /// </summary>
    public class ExportWordListToFileResponse : ResponseBase
    {
        public MemoryStream Result { get; set; }
    }
}