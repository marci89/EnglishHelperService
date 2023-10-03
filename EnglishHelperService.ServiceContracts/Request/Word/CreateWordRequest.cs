namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Create word request by logined user id
    /// </summary>
    public class CreateWordRequest
    {
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
