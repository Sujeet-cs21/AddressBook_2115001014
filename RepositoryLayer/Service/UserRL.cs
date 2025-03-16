using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly AddressBookContext _context;

        public UserRL(AddressBookContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUser(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}