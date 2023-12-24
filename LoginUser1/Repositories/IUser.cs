using LoginUser.Models;

namespace LoginUser.Repositories
{
    public interface IUser
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<UserAddResult> AddUserAsync(User user);
    }
}

