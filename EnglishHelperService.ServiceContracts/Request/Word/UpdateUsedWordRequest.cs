namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Update used word when learning
    /// </summary>
    public class UpdateUsedWordRequest
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Is corrent or incorrect?
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}
