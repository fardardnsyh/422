using Blogedium_api.Modals;
using Blogedium_api.Data;
using Microsoft.EntityFrameworkCore;
using Blogedium_api.Interfaces.Repository;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Blogedium_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserModal?> FindUserByEmailAddress (string emaildddress)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == emaildddress);
        }

        public async Task<UserModal> CreateUser (UserModal userModal)
        {
            userModal.Password = BCrypt.Net.BCrypt.HashPassword(userModal.Password);
            _context.Users.Add(userModal);
            await _context.SaveChangesAsync();
            return userModal;       
        }

        public async Task<UserModal?> FindUser (int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<UserModal> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null){
                 _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<IEnumerable<UserModal>> GetAllUsers ()
        {
            return await _context.Users.ToListAsync();
        } 
    }
}