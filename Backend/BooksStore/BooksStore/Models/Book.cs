namespace BooksStore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
    }
}
