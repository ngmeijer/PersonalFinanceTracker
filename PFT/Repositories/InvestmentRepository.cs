using MySql.Data.MySqlClient;
using PFT.Data;
using PFT.Models;

namespace PFT.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private PFTContext _context;
        private MySqlConnection _connection;

        public InvestmentRepository(PFTContext context) 
        {
            _context = context;
        }

        public async Task AddInvestmentAsync(Investment investment)
        {
            await _connection.OpenAsync();

            MySqlCommand command = new MySqlCommand(@"INSERT INTO Investments (Symbol, Quantity, Type) VALUES (@symbol, @quantity, @type)", _connection);
             
            command.Parameters.AddWithValue("@symbol", investment.Symbol);
            command.Parameters.AddWithValue("@quantity", investment.AmountHeld);
            command.Parameters.AddWithValue("@type", investment.Type);
             
            await command.ExecuteNonQueryAsync();
         }

        public Dictionary<string, Investment> GetAllInvestments()
        {
            return null;
        }
    }
}
