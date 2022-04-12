using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using PublisherDomain;
using System.Drawing;
using System.Linq.Expressions;

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
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Ignore(p => p.BasePrice);

        }



        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("nvarchar(100)");
            configurationBuilder.Properties<BookGenre>().HaveConversion<string>();
            configurationBuilder.Properties<Color>().HaveConversion(typeof(ColorToStringConverter));

        }


    }
}
public class ColorToStringConverter : ValueConverter<Color, string>
{
    public ColorToStringConverter() : base(ColorString, ColorStruct) { }

    private static Expression<Func<Color, string>> ColorString = v => new String(v.Name);
    private static Expression<Func<string, Color>> ColorStruct = v => Color.FromName(v);

}
public enum BookGenre
{
    Adventure = 5,
    HistoricalFiction = 6,
    History = 7,
    Memoir = 3,
    Mystery = 2,
    ScienceFiction = 1,
    YoungAdult = 4,
}
