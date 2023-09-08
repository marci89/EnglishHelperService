namespace EnglishHelperService.ServiceContracts
{
	public class PagedListRequest
	{
		private const int MaxPageSize = 10;
		public int PageNumber { get; set; } = 1;
		private int _pageSize = 10;

		public string FieldName { get; set; }
		public bool IsDescending { get; set; }

		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
		}
	}

}
