namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List word with filter
    /// </summary>
    public class ListWordWithFilterRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The words count
        /// </summary>
        public int WordNumber { get; set; }
        /// <summary>
        /// Word ordering enum type
        /// </summary>
        public WordOrderingType OrderType { get; set; }
    }
}
