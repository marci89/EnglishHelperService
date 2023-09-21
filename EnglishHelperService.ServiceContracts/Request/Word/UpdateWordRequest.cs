namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Update word request
    /// </summary>
    public class UpdateWordRequest
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string EnglishText { get; set; }
        public string HungarianText { get; set; }
        public int CorrectCount { get; set; }
        public int IncorrectCount { get; set; }
    }
}
