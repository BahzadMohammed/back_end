using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using news_api.Data;
using news_api.Interfaces;
using news_api.model;

namespace news_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        // #1
        public async Task<IEnumerable<User?>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // #2
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // #3
        public async Task<User?> AddUserAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // #4 
        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existUser = await _context.Users.FindAsync(id);
            if (existUser == null) return null;

            existUser.Email = user.Email;
            if(user.PasswordHash != null) {
                existUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

            await _context.SaveChangesAsync();
            return existUser;
        }

        // #5
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // #6
        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }
    }
}