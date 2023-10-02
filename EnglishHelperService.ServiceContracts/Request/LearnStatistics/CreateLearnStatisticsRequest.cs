namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create learn statistics request
    /// </summary>
    public class CreateLearnStatisticsRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
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
