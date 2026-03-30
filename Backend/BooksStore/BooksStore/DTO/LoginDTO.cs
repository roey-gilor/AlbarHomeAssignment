using System.ComponentModel.DataAnnotations;
namespace BooksStore.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "שם משתמש הוא שדה חובה")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "סיסמא היא שדה חובה")]
        public string Password { get; set; } = string.Empty;
    }
}
