namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Update word response
    /// </summary>
    public class UpdateWordResponse : ResponseBase
    {
        public Word Result { get; set; }
    }
}
