using System.ComponentModel.DataAnnotations;
namespace BooksStore.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = "שם ספר הוא שדה חובה")]
        [StringLength(100, ErrorMessage = "שם ספר לא יכול לעלות על 100 תווים")]
        public string BookName { get; set; } = string.Empty;

        [Required(ErrorMessage = "קטגוריה היא שדה חובה")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "מחיר הוא שדה חובה")]
        [Range(0.1, 10000, ErrorMessage = "מחיר חייב להיות גדול מאפס")]
        public decimal Price { get; set; }

        [Range(0, 10000, ErrorMessage = "כמות במלאי לא יכולה להיות שלילית")]
        public int UnitsInStock { get; set; }
    }
}
