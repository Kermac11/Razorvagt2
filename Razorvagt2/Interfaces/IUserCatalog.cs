using Razorvagt2.Models;

namespace Razorvagt2.Interfaces
{
    public interface IUserCatalog
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserFromID(int ID);

        Task<bool> CreateUser(User user);
        
        Task<bool> UpdateUser(int userid, User user);

        Task<User> DeleteUser(int userid);
    }
}
