#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublisherData;

namespace PubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsWithBooksController : ControllerBase
    {
        private readonly PubContext _context;

        public AuthorsWithBooksController(PubContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorWithBooksDTO>>> GetAuthors()
        {

            return await _context.Authors
            .Select(a => new AuthorWithBooksDTO
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Books = a.Books.Select
               (b => new BooksDTO
               {
                   BookId = b.BookId,
                   BasePrice = b.BasePrice,
                   Title = b.Title,
                   PublishDate = b.PublishDate
               }
               ).ToList()
            }).ToListAsync();
        }


        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorWithBooksDTO>> GetAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books)
              .Select(a => new AuthorWithBooksDTO
              {
                  AuthorId = a.AuthorId,
                  FirstName = a.FirstName,
                  LastName = a.LastName,
                  Books = a.Books.Select(b => new BooksDTO
                  {
                      BookId = b.BookId,
                      BasePrice = b.BasePrice,
                      Title = b.Title,
                      PublishDate = b.PublishDate
                  }).ToList()

              })
                .FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

    }
}
