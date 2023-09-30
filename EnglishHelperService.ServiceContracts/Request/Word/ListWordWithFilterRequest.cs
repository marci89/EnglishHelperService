namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List word with filter
    /// </summary>
    public class ListWordWithFilterRequest
    {
        public long UserId { get; set; }
        public int WordNumber { get; set; }
        public WordOrderingType OrderType { get; set; }
    }
}
