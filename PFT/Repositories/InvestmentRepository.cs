using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using PFT.Data;
using PFT.Models;
using System.Data;

namespace PFT.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private PFTContext _context;
        private string _connectionString;

        public InvestmentRepository(IConfiguration config) 
        {
            if(config == null) throw new ArgumentNullException("config is null");

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task AddInvestmentAsync(Investment investment)
        {
            using var connection = new SqlConnection( _connectionString );

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(@"INSERT INTO Investments (Symbol, Quantity, Type) VALUES (@symbol, @quantity, @type)", connection);
             
            command.Parameters.AddWithValue("@symbol", investment.Symbol);
            command.Parameters.AddWithValue("@quantity", investment.Quantity);
            command.Parameters.AddWithValue("@type", investment.Type);
             
            await command.ExecuteNonQueryAsync();
         }

        public async Task<Dictionary<string, Investment>> GetAllInvestmentsAsync()
        {
            Dictionary<string, Investment> data = new();

            using var connection = new MySqlConnection(_connectionString);

            await connection.OpenAsync();

            using var command = new MySqlCommand();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string symbol = reader.GetString("Symbol");
                data.Add(symbol, new Investment
                {
                    Id = reader.GetInt32("Id"),
                    Type = (InvestmentType)reader.GetInt32("Type"),
                    Quantity = reader.GetFloat("quantity")
                });
            }

            return data;
        }
    }
}
