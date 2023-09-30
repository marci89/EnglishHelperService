namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Word object for client
    /// </summary>
    public class Word
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string EnglishText { get; set; }
        public string HungarianText { get; set; }
        public int CorrectCount { get; set; }
        public int IncorrectCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUse { get; set; }

        /// <summary>
        /// Correct and incorrect values balance
        /// </summary>
        public int Balance
        {
            get
            {
                return this.CorrectCount - this.IncorrectCount;
            }
        }
    }
}
