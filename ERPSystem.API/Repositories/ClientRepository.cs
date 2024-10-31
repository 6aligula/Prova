using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ERPSystem.API.Models;
using Microsoft.Extensions.Configuration;

namespace ERP.Api.Repositories
{
    public class ClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("ERPDatabase")))
            {
                return await db.QueryAsync<Client>("SELECT * FROM Client");
            }
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("ERPDatabase")))
            {
                return await db.QuerySingleOrDefaultAsync<Client>("SELECT * FROM Client WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<int> AddClientAsync(Client client)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("ERPDatabase")))
            {
                var sql = "INSERT INTO Client (Name, Email, Phone, CreatedAt) VALUES (@Name, @Email, @Phone, @CreatedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await db.ExecuteScalarAsync<int>(sql, client);
            }
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("ERPDatabase")))
            {
                var sql = "UPDATE Client SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @Id";
                var rowsAffected = await db.ExecuteAsync(sql, client);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("ERPDatabase")))
            {
                var sql = "DELETE FROM Client WHERE Id = @Id";
                var rowsAffected = await db.ExecuteAsync(sql, new { Id = id });
                return rowsAffected > 0;
            }
        }
    }
}
