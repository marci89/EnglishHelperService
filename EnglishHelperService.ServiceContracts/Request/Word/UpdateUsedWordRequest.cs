namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// Update used word when learning
    /// </summary>
    public class UpdateUsedWordRequest
    {
        public long Id { get; set; }
        public bool IsCorrect { get; set; }
    }
}
