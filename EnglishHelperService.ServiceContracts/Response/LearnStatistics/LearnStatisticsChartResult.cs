namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Class for chart diagram Result
    /// </summary>
    public class LearnStatisticsChartResult
    {
        /// <summary>
        /// Label list for chart diagram 
        /// </summary>
        public List<string> ChartLabel { get; set; }
        /// <summary>
        /// Flashcard chart data
        /// </summary>
        public List<string> FlashcardChartData { get; set; }
        /// <summary>
        /// Typing chart data
        /// </summary>
        public List<string> TypingChartData { get; set; }
        /// <summary>
        /// Selection chart data
        /// </summary>
        public List<string> SelectionChartData { get; set; }
        /// <summary>
        /// Listening chart data
        /// </summary>
        public List<string> ListeningChartData { get; set; }
    }
}
