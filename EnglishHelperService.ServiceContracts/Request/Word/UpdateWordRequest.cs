namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Update word request
    /// </summary>
    public class UpdateWordRequest
    {
        /// <summary>
        /// Word identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Word's user id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// English text
        /// </summary>
        public string EnglishText { get; set; }
        /// <summary>
        /// Hungarian text
        /// </summary>
        public string HungarianText { get; set; }
        /// <summary>
        /// Good counts
        /// </summary>
        public int CorrectCount { get; set; }
        /// <summary>
        /// Bad counts
        /// </summary>
        public int IncorrectCount { get; set; }
    }
}
