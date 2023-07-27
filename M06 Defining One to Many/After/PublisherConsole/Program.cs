// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
_context.Database.EnsureDeleted();
_context.Database.EnsureCreated();

AddBook();

void AddBook()
{
    var book = new Book { Title = "How to crash your app" };
    _context.Books.Add(book);
    _context.SaveChanges();
}