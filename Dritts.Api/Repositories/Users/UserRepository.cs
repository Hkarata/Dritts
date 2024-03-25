using Dritts.Api.Contracts.Requests;
using Dritts.Api.Data;
using Dritts.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dritts.Api.Repositories.Users
{
    public class UserRepository(AppDbContext _context) : IUserRepository
    {
        public async Task<Guid> CreateUser(CreateUser user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> GetUserByFullName(string firstName, string middleName, string lastName)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.FirstName == firstName && u.MiddleName == middleName && u.LastName == lastName);
        }


        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id) ?? null;
        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber) ?? null;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
