namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create learn statistics request by logined user id
    /// </summary>
    public class CreateLearnStatisticsRequest
    {
        /// <summary>
        /// Good counts
        /// </summary>
        public int CorrectCount { get; set; }
        /// <summary>
        /// Bad counts
        /// </summary>
        public int IncorrectCount { get; set; }
        /// <summary>
        /// Result percent
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// Learn mode type
        /// </summary>
        public LearnModeType LearnMode { get; set; }
    }
}
