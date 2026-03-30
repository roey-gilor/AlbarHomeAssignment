using BooksStore.DTO;

namespace BooksStore.BusinessLogic
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> SearchBooksByNameAsync(string bookName);
        Task<List<BookDTO>> SearchBooksByCategoryAsync(string category);
        Task<BookDTO> CreateNewBookAsync(BookDTO dto);
        Task<bool> UpdateBookAsync(int id, BookDTO dto);
    }
}
