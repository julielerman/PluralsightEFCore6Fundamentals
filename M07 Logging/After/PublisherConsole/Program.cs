
using PublisherData;

PubContext _context = new PubContext();
//this assumes you are working with the populated
//database created in previous module

GetAuthors();

void GetAuthors()
{
    var authors = _context.Authors.ToList();
}