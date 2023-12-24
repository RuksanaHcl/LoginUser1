using LoginUser.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LoginUser.Repositories
{
    public enum UserAddResult
    {
        Success = 1,
        UsernameExists = 0,
        Error = -1
    }

    public class UserRepository : IUser
    {
        private readonly DbContextClass _dbContext;

        public UserRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<UserAddResult> AddUserAsync(User user)
        {
            try
            {
                if (await _dbContext.Users.AnyAsync(u => u.UserName == user.UserName))
                {
                    return UserAddResult.UsernameExists;
                }

                string hashedPassword = HashPassword(user.Password);

                user.Password = hashedPassword;
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return UserAddResult.Success;
            }
            catch (Exception)
            {
                return UserAddResult.Error;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }
    }
}
