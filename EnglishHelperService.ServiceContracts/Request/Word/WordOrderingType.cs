namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Word ordering enum type
    /// </summary>
    public enum WordOrderingType
    {
        /// <summary>
        /// it can be anything
        /// </summary>
        Any = 1,
        /// <summary>
        /// starting the newest words
        /// </summary>
        Newest = 2,
        /// <summary>
        /// starting the oldest words
        /// </summary>
        Oldest = 3,
        /// <summary>
        /// starting from the best good-bad balance
        /// </summary>
        Best = 4,
        /// <summary>
        /// starting from the worst good-bad balance
        /// </summary>
        Worst = 5,
    }
}
