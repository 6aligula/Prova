using Dapper;
using ERPSystem.API.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ERPSystem.API.Services
{
    public class ClientService
    {
        private readonly string _connectionString;

        public ClientService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ERPDatabase");
        }

        public void AddClient(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Client (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)";
                connection.Execute(query, client);
            }
        }
        public Client GetClientById(int clientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Client WHERE ClientID = @ClientID";
                return connection.QuerySingleOrDefault<Client>(query, new { ClientID = clientId });
            }
        }
        public void UpdateClient(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Client SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address WHERE ClientID = @ClientID";
                connection.Execute(query, client);
            }
        }
        public void DeleteClient(int clientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Client WHERE ClientID = @ClientID";
                connection.Execute(query, new { ClientID = clientId });
            }
        }

        public IEnumerable<Client> GetClients()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Client";
                return connection.Query<Client>(query).ToList();
            }
        }
    }
}
