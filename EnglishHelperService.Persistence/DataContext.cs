using EnglishHelperService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.Persistence
{
    /// <summary>
    /// Database context
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the tables with the current table's configuratuion class. This line handle all of tables where exists the table name.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
