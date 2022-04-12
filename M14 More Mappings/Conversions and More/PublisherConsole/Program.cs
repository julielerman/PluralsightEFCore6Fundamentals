using PublisherData;

PubContext _context = new PubContext(); //existing database

RetrieveAndUpdateOneBook();
void RetrieveAndUpdateOneBook()
{
    var thebook = _context.Books.FirstOrDefault();
    thebook.BasePrice = 15.00M;
    thebook.Title += "!";
    _context.SaveChanges();
}