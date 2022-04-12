using Microsoft.EntityFrameworkCore;
using PublisherData;

PubContext _context = new PubContext(); //existing database

CancelBookWithDefaultTransaction(8);
void CancelBookWithDefaultTransaction(int bookid)
{
    //get a list of artists working on book covers for this book
    var artists = _context.Artists
        .Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
    foreach (var artist in artists)
        artist.Notes +=
            Environment.NewLine +
            $"Assigned book { bookid} was cancelled on { DateTime.Today.Date} ";
    //by default, raw sql methods are not in transactions
    _context.Database.ExecuteSqlInterpolated($"Delete from books where bookid={bookid}");

    _context.SaveChanges();
}




CancelBookWithCustomTransaction(12);
void CancelBookWithCustomTransaction(int bookid)
{
    using var transaction = _context.Database.BeginTransaction();
    try
    {
        //get a list of artists working on book covers for this book
        var artists = _context.Artists
            .Where(a => a.Covers.Any(cover => cover.BookId == bookid)).ToList();
        foreach (var artist in artists)
            artist.Notes +=
                Environment.NewLine +
                $"Assigned book { bookid} was cancelled on { DateTime.Today.Date} ";                                                                                                                         //by default, raw sql methods execute in their own transaction immediately
        
        _context.Database.ExecuteSqlInterpolated($"Delete from books where bookid={bookid}");

        _context.SaveChanges();
        transaction.Commit();
    }
    catch (Exception)
    {
        // TODO: Handle failure
    }
}