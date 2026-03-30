using BooksStore.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BooksStore.DTO;
using BooksStore.BusinessLogic;

namespace BooksStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                string? token = await _userService.LoginAsync(dto);
                if (token == null)
                    return Unauthorized(new { message = "Wrong user name or password" });
                return Ok(new { token = token });
            }
            catch (Exception)
            {
                return StatusCode(500, "Unknown LogIn Error");
            }
        }
    }
}
