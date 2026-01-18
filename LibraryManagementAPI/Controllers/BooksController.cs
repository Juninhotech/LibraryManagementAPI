using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.IServices;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks([FromQuery] string? search)
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync(search);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books");
                return StatusCode(500, "An error occurred while retrieving books");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);

                if (book == null)
                    return NotFound($"Book with ID {id} not found");

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving book with ID {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the book");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var book = await _bookService.CreateBookAsync(createBookDto);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book");
                return StatusCode(500, "An error occurred while creating the book");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var book = await _bookService.UpdateBookAsync(id, updateBookDto);

                if (book == null)
                    return NotFound($"Book with ID {id} not found");

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book with ID {Id}", id);
                return StatusCode(500, "An error occurred while updating the book");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var deleted = await _bookService.DeleteBookAsync(id);

                if (!deleted)
                    return NotFound($"Book with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book with ID {Id}", id);
                return StatusCode(500, "An error occurred while deleting the book");
            }
        }
    }
}

