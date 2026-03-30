using BooksStore.DAL;
using BooksStore.Models;
using BooksStore.DTO;
using Microsoft.EntityFrameworkCore;
using BooksStore.Mappers;

namespace BooksStore.BusinessLogic
{
    public class BookService : IBookService
    {
        private readonly BookStoreDbContext _context;
        public BookService(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            try
            {
                List<Book> books = await _context.Books.ToListAsync();
                return books.Select(p => p.ToDTO()).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured while retrieving all books:", ex);
            }
        }

        public async Task<BookDTO> SearchBooksByNameAsync(string bookName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(bookName))
                    return null;

                Book? book = await _context.Books
                        .FirstOrDefaultAsync(p => p.BookName == bookName);
                if (book == null)
                    return null;

                return book.ToDTO();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured while searching books:", ex);
            }
        }

        public async Task<List<BookDTO>> SearchBooksByCategoryAsync(string category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category))
                    return new List<BookDTO>();

                List<Book> books = await _context.Books
                    .Where(p => p.Category.Contains(category))
                    .ToListAsync();

                return books.Select(p => p.ToDTO()).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured while Searching books By category:", ex);
            }
        }

        public async Task<BookDTO> CreateNewBookAsync(BookDTO dto)
        {
            try
            {
                bool nameExists = await _context.Books.AnyAsync(b => b.BookName == dto.BookName);
                if (nameExists)
                    return null;

                Book book = dto.ToEntity();
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return book.ToDTO();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while adding new book:", ex);
            }
        }

        public async Task<bool> UpdateBookAsync(int id, BookDTO dto)
        {
            try
            {
                Book? book = await _context.Books.FindAsync(id);
                if (book == null)
                    return false;

                bool nameExists = await _context.Books.AnyAsync(b => b.BookName == dto.BookName && b.BookId != id);
                if (nameExists)
                    throw new ApplicationException($"Book with the name {dto.BookName} already exists");

                book.BookName = dto.BookName;
                book.Category = dto.Category;
                book.Price = dto.Price;
                book.UnitsInStock = dto.UnitsInStock;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while trying to update book:", ex);
            }
        }
    }
}
