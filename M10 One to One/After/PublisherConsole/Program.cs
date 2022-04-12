using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext(); //existing database

//GetAllBooksWithTheirCovers();
void GetAllBooksWithTheirCovers()
{
    var booksandcovers = _context.Books.Include(b => b.Cover).ToList();
    booksandcovers.ForEach(book =>
     Console.WriteLine(
         book.Title +
         (book.Cover == null ? ": No cover yet" : ":" + book.Cover.DesignIdeas)));
}
//GetAllBooksThatHaveCovers();
void GetAllBooksThatHaveCovers()
{
    var booksandcovers = _context.Books.Include(b => b.Cover).Where(b => b.Cover != null).ToList();
    booksandcovers.ForEach(book =>
       Console.WriteLine(book.Title + ":" + book.Cover.DesignIdeas));
}
//ProjectBooksThatHaveCovers();
void ProjectBooksThatHaveCovers()
{
    var anon = _context.Books.Where(b => b.Cover != null)
      .Select(b => new { b.Title, b.Cover.DesignIdeas })
      .ToList();
    anon.ForEach(b =>
      Console.WriteLine(b.Title + ": " + b.DesignIdeas));

}

//MultiLevelInclude();
void MultiLevelInclude()
{
    var authorGraph = _context.Authors.AsNoTracking()
        .Include(a => a.Books)
        .ThenInclude(b => b.Cover)
        .ThenInclude(c => c.Artists)
        .FirstOrDefault(a => a.AuthorId == 1);

    Console.WriteLine(authorGraph?.FirstName + " " + authorGraph?.LastName);
    foreach (var book in authorGraph.Books)
    {
        Console.WriteLine("Book:" + book.Title);
        if (book.Cover != null)
        {
            Console.WriteLine("Design Ideas: " + book.Cover.DesignIdeas);
            Console.Write("Artist(s):");
            book.Cover.Artists.ForEach(a => Console.Write(a.LastName + " "));

        }
    }
};



//NewBookAndCover();
void NewBookAndCover()
{
    var book = new Book {AuthorId=1, Title = "Call Me Ishtar",
                         PublishDate = new DateTime(1973, 1, 1) };
    book.Cover = new Cover { DesignIdeas = "Image of Ishtar?" };
    _context.Books.Add(book);   
    _context.SaveChanges();
}

//AddCoverToExistingBook();
void AddCoverToExistingBook()
{
    var book = _context.Books.Find(7); //Wool
    book.Cover = new Cover { DesignIdeas = "A wool scouring pad" };
     _context.SaveChanges();
}


//AddCoverToExistingBookThatHasAnUnTrackedCover();
void AddCoverToExistingBookThatHasAnUnTrackedCover()
{
    var book = _context.Books.Find(5); //The Never
    book.Cover = new Cover { DesignIdeas = "A spiral" };
    _context.SaveChanges();
}

//AddCoverToExistingBookWithTrackedCover();
void AddCoverToExistingBookWithTrackedCover()
{
    var book = _context.Books.Include(b => b.Cover)
                             .FirstOrDefault(b => b.BookId == 5); //The Never
    book.Cover = new Cover { DesignIdeas = "A spiral" };
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
}

//ProtectingFromUniqueFK();
void ProtectingFromUniqueFK()
{
    var TheNeverDesignIdeas = "A spirally spiral";
    var book = _context.Books.Include(b => b.Cover)
                             .FirstOrDefault(b => b.BookId == 5); //The Never
    if (book.Cover != null)
    {
        book.Cover.DesignIdeas = TheNeverDesignIdeas;
    }
    else
    {
        book.Cover = new Cover { DesignIdeas = "A spirally spiral" };
    }
    _context.SaveChanges();
}


//MoveCoverFromOneBookToAnother();

void MoveCoverFromOneBookToAnother()
{
    ///"we like birds"coverid 5, currenlty assigned to The Never bookid 5
    var cover = _context.Covers.Include(c=>c.Book).FirstOrDefault(c=>c.CoverId==5);
    var newBook = _context.Books.Find(3);
    cover.Book=newBook;
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
}

DeleteCoverFromBook();

void DeleteCoverFromBook()
{
   var book=_context.Books.Include(b=>b.Cover).FirstOrDefault(b=>b.BookId==5);
   book.Cover=null;
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
}