#nullable disable
using Microsoft.AspNetCore.Mvc;
using PublisherDomain;

namespace PubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataLogic _dl;

        public AuthorsController(DataLogic dl)
        {
            _dl = dl;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authorDTOList = await _dl.GetAllAuthors();
            return authorDTOList;
        }
 
        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var authorDTO = await _dl.GetAuthorById(id);

            if (authorDTO == null)
            {
                return NotFound();
            }
            return authorDTO;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorDTO authorDTO)
        {
            if (id != authorDTO.AuthorId)
            {
                return BadRequest();
            }
            bool response = await _dl.UpdateAuthor(authorDTO);
            if (response == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDTO authorDTO)
        {
            AuthorDTO newAuthorDTO = await _dl.SaveNewAuthor(authorDTO);
            return CreatedAtAction("GetAuthor", new { id = newAuthorDTO.AuthorId }, newAuthorDTO);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (await _dl.DeleteAuthor(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
