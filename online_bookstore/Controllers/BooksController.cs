using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online_bookstore.Data;
using online_bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_bookstore.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(
            [FromQuery] string? name,
            [FromQuery] string? genre,
            [FromQuery] string? author)
        {
            // Start with all books including related Genre and Author for filtering
            var query = _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .AsQueryable();

            // Filter by title (case insensitive, partial match)
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => b.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by genre name (case insensitive, partial match)
            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Name.Contains(genre, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by author name (case insensitive, partial match)
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Name.Contains(author, StringComparison.OrdinalIgnoreCase));
            }

            var books = await query.ToListAsync();

            if (books == null || books.Count == 0)
            {
                return NotFound(new { message = "No books found matching the criteria." });
            }

            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { result = "Book record was not found." });
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(new { message = "it is workign fine" } );
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Please fill the form correctly.", errors = ModelState });
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, new
            {
                message = "Book created successfully.",
                data = book
            });
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book was not found in the database." });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Book '{book.Title}' was deleted successfully." });
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
