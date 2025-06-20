using BCrypt.Net;
using FinanceAPI.Data;
using FinanceAPI.Models;
using MongoDB.Driver;

namespace FinanceAPI.Services
{
    public class AuthService
    {
        private readonly MongoDbContext _context;

        public AuthService(MongoDbContext context)
        {
            _context = context;
        }

        // Register a new user (returns created User)
        public async Task<User> RegisterAsync(string email, string password, string name)
        {
            // Check if user already exists
            var existing = await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (existing != null)
                throw new Exception("Email already in use.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Name = name
            };

            await _context.Users.InsertOneAsync(user);
            return user;
        }

        // Validate credentials (returns user if valid, null otherwise)
        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null) return null;

            bool valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return valid ? user : null;
        }
    }
}
