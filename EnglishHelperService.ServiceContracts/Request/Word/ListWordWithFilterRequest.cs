namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List word with filter by logined user id
    /// </summary>
    public class ListWordWithFilterRequest
    {
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
