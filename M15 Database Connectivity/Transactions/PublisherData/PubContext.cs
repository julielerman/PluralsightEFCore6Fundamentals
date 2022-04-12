using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase")
                .LogTo(Console.WriteLine,
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information)
            .EnableSensitiveDataLogging();
        }
    }
}
      
     
