namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List learn statistics response
    /// </summary>
    public class ListLearnStatisticsResponse : ResponseBase
    {
        public List<LearnStatistics> Result { get; set; }
    }
}
