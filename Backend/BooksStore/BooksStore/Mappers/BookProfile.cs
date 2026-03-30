using BooksStore.Models;
using BooksStore.DTO;

namespace BooksStore.Mappers
{
    public static class BookProfile
    {
        //Convert Entity object To DTO object
        public static BookDTO ToDTO(this Book product)
        {
            return new BookDTO
            {
                BookId = product.BookId,
                BookName = product.BookName,
                Category = product.Category,
                Price = product.Price,
                UnitsInStock = product.UnitsInStock
            };
        }

        //Convert DTO object To Entity object
        public static Book ToEntity(this BookDTO dto)
        {
            return new Book
            {
                BookName = dto.BookName,
                Category = dto.Category,
                Price = dto.Price,
                UnitsInStock = dto.UnitsInStock
            };
        }
    }
}
