namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Read word by id response
    /// </summary>
    public class ReadWordByIdResponse : ResponseBase
    {
        public Word Result { get; set; }
    }
}
