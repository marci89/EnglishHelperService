using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.Persistence
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
