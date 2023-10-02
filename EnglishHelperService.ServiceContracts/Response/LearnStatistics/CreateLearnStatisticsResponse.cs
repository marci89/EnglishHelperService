namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create learn statistics response
    /// </summary>
    public class CreateLearnStatisticsResponse : ResponseBase
    {
        public LearnStatistics Result { get; set; }
    }
}
