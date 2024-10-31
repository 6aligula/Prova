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
    }
}
