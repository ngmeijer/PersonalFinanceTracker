using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public InvestmentRepository(PFTContext context)
        {
            _context = context;
        }

        public async Task AddInvestmentAsync(Investment newInvestment)
        {
            _context.Investments.Add(newInvestment);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeInvestmentAsync(Investment requestedInvestment)
        {
            var investment = await _context.Investments.FirstOrDefaultAsync(inv => inv.Symbol == requestedInvestment.Symbol);
            if (investment == null)
                throw new KeyNotFoundException($"Investment with symbol {requestedInvestment.Symbol} was not found in the database");

            investment.Quantity = requestedInvestment.Quantity;

            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<string, Investment>> GetAllInvestmentsAsync()
        {
            Dictionary<string, Investment> data = await _context.Investments.ToDictionaryAsync(investment => investment.Symbol);

            return data;       
        }

        public async Task RemoveInvestmentAsync(string symbolToDelete)
        {
            var investment = await _context.Investments.FirstOrDefaultAsync(inv => inv.Symbol == symbolToDelete);
            if (investment == null)
                throw new KeyNotFoundException($"Investment with symbol {symbolToDelete} was not found in the database");

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfInvestmentExists(string symbol)
        {
            return await _context.Investments.AnyAsync(i => i.Symbol == symbol);
        }
    }
}
