namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create word response
    /// </summary>
    public class CreateWordResponse : ResponseBase
    {
        public Word Result { get; set; }
    }
}