namespace EnglishHelperService.Persistence.Entities
{
	public class Word
	{
		public long Id { get; set; }
		public long UserId { get; set; }
		public string EnglishText { get; set; }
		public string HungarianText { get; set; }
		public int CorrectCount { get; set; }
		public int IncorrectCount { get; set; }
		public DateTime Created { get; set; }
		public DateTime? LastUse { get; set; }
		public virtual User User { get; set; }
	}
}
