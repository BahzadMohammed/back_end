using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using news_api.model;

namespace news_api.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User?>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> AddUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> AuthenticateUserAsync(string username, string password);
    }
}