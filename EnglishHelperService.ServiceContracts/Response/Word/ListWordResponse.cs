namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List word response
    /// </summary>
    public class ListWordResponse : ResponseBase
    {
        public List<Word> Result { get; set; }
    }
}