namespace EnglishHelperService.Persistence.Entities
{
    /// <summary>
    /// Learn statistics entity
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
        /// Learn statistics created date
        /// </summary
        public DateTime Created { get; set; }

        #region Navigation propeties

        /// <summary>
        /// Learn statistics's User
        /// </summary>
        public virtual User User { get; set; }

        #endregion
    }
}
