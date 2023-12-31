﻿namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Paged list response for paginator
	/// </summary>
	public class PagedList<T>
	{
		public PagedList(IEnumerable<T> items, long count, int pageNumber, int pageSize)
		{
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			PageSize = pageSize;
			TotalCount = count;
			Items = items.ToList();
		}

		public List<T> Items { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public long TotalCount { get; set; }
	}
}