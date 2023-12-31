﻿namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Word object for client
    /// </summary>
    public class Word
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
        /// <summary>
        /// Word created date
        /// </summary
        public DateTime Created { get; set; }
        /// <summary>
        /// Word last use date
        /// </summary>
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
