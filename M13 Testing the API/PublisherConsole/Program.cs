using Microsoft.EntityFrameworkCore;
using PublisherData;

PubContext _context = new PubContext(); //existing database


DeleteCover(10);

void DeleteCover(int coverId)
{
    var rowCount = _context.Database.ExecuteSqlRaw("DeleteCover {0}", coverId);
    Console.WriteLine(rowCount);
}



GetAuthorsByArtist();

void GetAuthorsByArtist()
{
    var authorartists=_context.AuthorsByArtist.ToList();
    var oneauthorartists = _context.AuthorsByArtist.FirstOrDefault();
    var Kauthorartists = _context.AuthorsByArtist
                                 .Where(a=>a.Artist.StartsWith("K")).ToList();
    var debugView = _context.ChangeTracker.DebugView.ShortView;
} 

 

RawSqlStoredProc();
void RawSqlStoredProc()
{
    var authors = _context.Authors
        .FromSqlRaw("AuthorsPublishedinYearRange {0}, {1}", 2010, 2015)
        .ToList();
}

InterpolatedSqlStoredProc();
void InterpolatedSqlStoredProc()
{
    int start = 2010;
    int end = 2015;
    var authors = _context.Authors
    .FromSqlInterpolated($"AuthorsPublishedinYearRange {start}, {end}")
    .ToList();
}


//SimpleRawSQL();
void SimpleRawSQL()
{
    var authors = _context.Authors.FromSqlRaw("select * from authors").OrderBy(a => a.LastName).ToList();
}

//ConcatenatedRawSql_Unsafe(); //There is no safe way with concatentation!
void ConcatenatedRawSql_Unsafe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '" + lastnameStart + "%'")
        .OrderBy(a => a.LastName).TagWith("Concatenated_Unsafe").ToList();
}

//FormattedRawSql_Unsafe();
void FormattedRawSql_Unsafe()
{
  var lastnameStart = "L";
  var sql = String.Format("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart);
  var authors = _context.Authors.FromSqlRaw(sql)
      .OrderBy(a => a.LastName).TagWith("Formatted_Unsafe").ToList();
}

//FormattedRawSql_Safe();
void FormattedRawSql_Safe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
        .OrderBy(a => a.LastName).TagWith("Formatted_Safe").ToList();
}

//StringFromInterpolated_Unsafe();
void StringFromInterpolated_Unsafe()
{
    var lastnameStart = "L";
    string sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";
    var authors = _context.Authors.FromSqlRaw(sql)
        .OrderBy(a => a.LastName).TagWith("Interpolated_Unsafe").ToList();
}

//StringFromInterpolated_StillUnsafe();
void StringFromInterpolated_StillUnsafe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlRaw($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
        .OrderBy(a => a.LastName).TagWith("Interpolated_StillUnsafe").ToList();
}

//StringFromInterpolated_Safe();
void StringFromInterpolated_Safe()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlInterpolated($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
    .OrderBy(a => a.LastName).TagWith("Interpolated_Safe").ToList();
}

#if false
StringFromInterpolated_SoSafeItWontCompile();
void StringFromInterpolated_SoSafeItWontCompile()
{
    var lastnameStart = "L";
    var sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";
    var authors = _context.Authors.FromSqlInterpolated(sql)
    .OrderBy(a => a.LastName).TagWith("Interpolated_WontCompile").ToList();
}

FormattedWithInterpolated_SoSafeItWontCompile();
void FormattedWithInterpolated_SoSafeItWontCompile()
{
    var lastnameStart = "L";
    var authors = _context.Authors
        .FromSqlInterpolated
            ("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
        .OrderBy(a => a.LastName).TagWith("Interpolated_WontCompile").ToList();
}
#endif
