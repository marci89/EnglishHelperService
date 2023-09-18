namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create word request
    /// </summary>
    public class CreateWordRequest
    {
        public long UserId { get; set; }
        public string EnglishText { get; set; }
        public string HungarianText { get; set; }
    }
}
