namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create word request
    /// </summary>
    public class CreateWordRequest
    {
        /// <summary>
        /// User id
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
    }
}
