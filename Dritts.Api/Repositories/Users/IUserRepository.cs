using Dritts.Api.Contracts.Requests;
using Dritts.Api.Models;

namespace Dritts.Api.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(Guid id);
        Task<User?> GetUserByPhoneNumber(string phoneNumber);
        Task<bool> GetUserByFullName(string firstName, string middleName, string lastName);
        Task<IEnumerable<User>> GetUsers();
        Task<Guid> CreateUser(CreateUser user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(Guid id);

    }
}
