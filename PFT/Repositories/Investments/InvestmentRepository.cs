using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using PFT.Data;
using PFT.Models.Investments;
using System.Data;

namespace PFT.Repositories.Investments
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private PFTContext _context;
        private string _connectionString;

        public InvestmentRepository(IConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config is null");

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task AddInvestmentAsync(Investment investment)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(@"INSERT INTO Investments (Symbol, Quantity, Type) VALUES (@symbol, @quantity, @type)", connection);

            command.Parameters.AddWithValue("@symbol", investment.Symbol);
            command.Parameters.AddWithValue("@quantity", investment.Quantity);
            command.Parameters.AddWithValue("@type", investment.Type);

            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }

        public async Task ChangeInvestmentAsync(Investment investment)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(@"UPDATE Investments SET Quantity = @quantity WHERE Symbol = @symbol AND Type = @type", connection);

            command.Parameters.AddWithValue("@symbol", investment.Symbol);
            command.Parameters.AddWithValue("@quantity", investment.Quantity);
            command.Parameters.AddWithValue("@type", investment.Type);

            await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }

        public async Task<Dictionary<string, InvestmentWrapper>> GetAllInvestmentsAsync()
        {
            Dictionary<string, InvestmentWrapper> data = new();

            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand("SELECT Id, Symbol, Type, Quantity FROM Investments", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                string symbol = reader.GetString("Symbol");
                if (data.ContainsKey(symbol))
                    continue;

                data.Add(symbol, new InvestmentWrapper
                {
                    CachedData = new()
                    {
                        Id = reader.GetInt32("Id"),
                        Type = (InvestmentType)reader.GetInt32("Type"),
                        Quantity = reader.GetFloat("quantity")
                    }
                });
            }

            await connection.CloseAsync();

            return data;
        }

        public async Task RemoveInvestmentAsync(string symbol)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand($"DELETE FROM Investments WHERE Symbol = @symbol", connection);
            command.Parameters.AddWithValue("@symbol", symbol);

            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
    }
}
