using Sample.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Repository
{
    public interface IRepository
    {
        Task AddUser(Register register);
        Task<IEnumerable<Register>> GetAllUsers();
        Task<Register> LoginAsync(Register register);
        Task<Register> GetUserById(int id);
        void Update(Register register);
        void DeleteUser(int id);
        Task SaveChangesAsync();
        Task<bool> UserExists(int id);
    }
}
