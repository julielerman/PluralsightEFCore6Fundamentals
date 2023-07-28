using Microsoft.EntityFrameworkCore;
using PublisherData;

PubContext _context = new PubContext(); 
_context.Database.EnsureDeleted();
_context.Database.EnsureCreated();

RetrieveAndUpdateOneBook();
void RetrieveAndUpdateOneBook()
{
    var thebook = _context.Books.FirstOrDefault();
    thebook.BasePrice = 15.00M;
    thebook.Title += "!";
    _context.SaveChanges();
}