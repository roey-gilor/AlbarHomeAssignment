using BooksStore.DTO;

namespace BooksStore.BusinessLogic
{
    public interface IUserService
    {
        Task<string?> LoginAsync(LoginDTO loginDto);
    }
}
