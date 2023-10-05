namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List learn statistics datas for chart diagram response
    /// </summary>
    public class ListLearnStatisticsChartResponse : ResponseBase
    {
        public LearnStatisticsChartResult Result { get; set; }
    }
}
