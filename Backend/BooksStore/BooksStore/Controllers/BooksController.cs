using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BooksStore.DTO;
using BooksStore.BusinessLogic;
using Microsoft.AspNetCore.Authorization;

namespace BooksStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _productService;
        public BooksController(IBookService productService)
        {
            _productService = productService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetAllBooks()
        {
            try
            {
                List<BookDTO> books = await _productService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while retrieving all books:");
            }
        }

        // GET: api/books/search/name/{name}
        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<BookDTO>> SearchByName(string name)
        {
            try
            {
                BookDTO book = await _productService.SearchBooksByNameAsync(name);
                if (book == null) 
                    return NoContent();
                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while searching books");
            }
        }

        // GET: api/books/search/category/{category}
        [HttpGet("search/category/{category}")]
        public async Task<ActionResult<List<BookDTO>>> SearchByCategory(string category)
        {
            try
            {
                List<BookDTO> books = await _productService.SearchBooksByCategoryAsync(category);
                if (books.Count == 0)
                    return NoContent();
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while Searching books By category");
            }
        }

        // POST: api/books/create
        [HttpPost("Create")]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookDTO dto)
        {
            try
            {
                BookDTO? book = await _productService.CreateNewBookAsync(dto);
                if (book == null)
                    return StatusCode(409, $"Book with the name {dto.BookName} already exists");
                return Created($"api/books/create/{book.BookId}", book);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while adding new book");
            }
        }

        // PUT: api/books/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookDTO dto)
        {
            try
            {
                bool success = await _productService.UpdateBookAsync(id, dto);
                if (success)
                    return Ok(new { message = "book updated successfully" });

                return NotFound($"book with id {id} was not found");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while adding new book");
            }
        }
    }
}
