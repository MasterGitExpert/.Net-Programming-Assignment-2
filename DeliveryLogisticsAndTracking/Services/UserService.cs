using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DeliveryLogisticsAndTracking.Data;
using DeliveryLogisticsAndTracking.Models;
using System.Security.Cryptography;
using System.Text;

namespace DeliveryLogisticsAndTracking.Services
{
    /* <summary>
    Service class to manage Users and their credentials in the database.
    Handles CRUD operations, password hashing, login validation, and transactions.
    </summary>*/
    public class UserService
    {
        private readonly AppDbContext _context;

        // Constructor injects the application's DbContext.
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Retrieves all users from the database.
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Retrieves a user by their unique ID.
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            // Name validation
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Name cannot be empty");

            // UserType validation
            if (string.IsNullOrWhiteSpace(user.UserType))
                throw new ArgumentException("UserType cannot be empty");

            // Email validation
            if (!IsValidEmail(user.Email))
                throw new ArgumentException("Invalid email");

            // Phone validation
            if (!IsValidPhone(user.Phone))
                throw new ArgumentException("Invalid phone");

            // DOB validation
            if (user.DOB > DateTime.Now)
                throw new ArgumentException("DOB cannot be in the future");

            // Add user
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
        }

        private bool IsValidPhone(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) && phone.All(char.IsDigit);
        }

        // Updates an existing user's information in the database.
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Deletes a user from the database by ID.
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Adds a password hash entry for a user.
        // Assumes each user has only one password record.
        public async Task AddUserPasswordAsync(int userId, string passwordHash)
        {
            var userPass = new UserAndPass
            {
                UserId = userId,
                PasswordHash = passwordHash
            };

            _context.UserAndPasses.Add(userPass);
            await _context.SaveChangesAsync();
        }

        // Begins a database transaction. Useful for grouping multiple operations.
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        // Commits a previously started transaction.
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.CommitAsync();
        }

        // Rolls back a previously started transaction in case of errors.
        public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            await transaction.RollbackAsync();
        }

        // Validates a user's login credentials by comparing the stored password hash.
        public async Task<bool> ValidateLoginAsync(int userId, string password)
        {
            var userPass = await _context.UserAndPasses
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userPass == null)
                return false;

            var hashedInput = HashPassword(password.Trim());
            return userPass.PasswordHash == hashedInput;
        }

        // Generates a SHA256 hash of a plaintext password.
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Retrieves a user by their email address.
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}