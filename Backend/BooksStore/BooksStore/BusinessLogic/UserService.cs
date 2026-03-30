using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BooksStore.DAL;
using BooksStore.DTO;
using BooksStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BooksStore.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly BookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(BookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string?> LoginAsync(LoginDTO loginDto)
        {
            // Searching the user in the DB
            User? user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == loginDto.Username && u.Password == loginDto.Password);

            // Return null if user is Not found
            if (user == null)
                return null;

            // Updating user last Login to current time
            user.LastLogIn = DateTime.Now;
            await _context.SaveChangesAsync();

            // Creating the JWT
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Reading the key from appsettings.json
            string? keyInfo = _configuration["Jwt:Key"] ?? 
                throw new InvalidOperationException("JWT Key is missing in appsettings");
            byte[]? key = Encoding.ASCII.GetBytes(keyInfo);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // Inserting token into user's details for later use
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                
                Expires = DateTime.UtcNow.AddHours(2), // Setting expiracy to 2 hours
                Issuer = _configuration["Jwt:Issuer"], // Who created and signed the token
                Audience = _configuration["Jwt:Audience"], // Who the token is intended for
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
