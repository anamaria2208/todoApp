

using backend.Model;
using backend.Model.DTOs;

namespace backend.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync (User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByUsernameAsync(string username);

    }
}