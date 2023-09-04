using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.Persistence
{
	public class DataContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Word> Words { get; set; }

		public DataContext(DbContextOptions options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
		}
	}
}
