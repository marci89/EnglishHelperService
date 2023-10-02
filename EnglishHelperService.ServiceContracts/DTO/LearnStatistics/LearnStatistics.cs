namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Learn statistics object for client
    /// </summary>
    public class LearnStatistics
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// User id for statistics
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
        /// <summary>
        /// Word created date
        /// </summary
        public DateTime Created { get; set; }

        /// <summary>
        /// Correct and incorrect values totality
        /// </summary>
        public int AllCount
        {
            get
            {
                return this.CorrectCount + this.IncorrectCount;
            }
        }
    }
}
