using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
_context.Database.EnsureCreated();

var author = new Author { FirstName = "Rhoda", LastName = "Lerman" };
author.Books.Add(
    new Book
    {
        Title = "In God's Ear",
        PublishDate = new DateTime(1989, 3, 1)
    });
author.Books.Add(new Book
{
    Title = "Call Me Ishtar",
    PublishDate = new DateTime(1975, 3, 1)
});
_context.Authors.Add(author);
_context.SaveChanges();
