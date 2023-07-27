
using PublisherData;

PubContext _context = new PubContext();
_context.Database.EnsureDeleted();
_context.Database.EnsureCreated();


GetAuthors();

void GetAuthors()
{
    var authors = _context.Authors.ToList();
}