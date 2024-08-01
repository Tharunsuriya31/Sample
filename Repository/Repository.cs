using Sample.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Sample.Repository
{
    public class Repository : IRepository
    {
        private readonly string _context;
        public Repository(IConfiguration configuration)
        {
            _context = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Register>> GetAllUsers()
        {
            using (var connection = new SqlConnection(_context))
            {
                var users = await connection.QueryAsync<Register>("GetAllUser", commandType: CommandType.StoredProcedure);
                return users;
            }
        }

        public async Task<Register> GetUserById(int id)
        {
            using (var connection = new SqlConnection(_context))
            {
                var parameter = new { Id = id };
                var user = await connection.QuerySingleOrDefaultAsync<Register>("GetUserById", parameter, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<Register> LoginAsync(Register register)
        {
            using (var connection = new SqlConnection(_context))
            {
                var parameters = new
                {
                    register.Email,
                    register.Password
                };
                var result = await connection.QuerySingleOrDefaultAsync<Register>("Login", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task AddUser(Register register)
        {
            using (var connection = new SqlConnection(_context))
            {
                var parameter = new
                {
                    register.Email,
                    register.Password,
                    register.MobileNo,
                    register.IDNumber,
                    register.PermanentAddress,
                    register.TemporaryAddress,
                    register.State,
                    register.District,
                    register.Zip,
                    register.EorV,
                    register.RegistrationDate
                };
                await connection.ExecuteAsync("spAddUser", parameter, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(Register register)
        {
            using (var connection = new SqlConnection(_context))
            {
                var parameters = new
                {
                    register.Id,
                    register.Email,
                    register.Password,
                    register.MobileNo,
                    register.IDNumber,
                    register.PermanentAddress,
                    register.TemporaryAddress,
                    register.State,
                    register.District,
                    register.Zip,
                    register.EorV,
                    
                };
                connection.Execute("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_context))
            {
                var parameters = new { Id = id };
                connection.Execute("DeleteUserByid", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public async Task<bool> UserExists(int id)
        {
            using (var connection = new SqlConnection(_context))
            {
                var parameter = new { Id = id };
                var exists = await connection.ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Users WHERE Id = @Id", parameter);
                return exists;
            }
        }
    }
}
