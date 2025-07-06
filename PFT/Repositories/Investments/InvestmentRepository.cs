using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public Task<Investment> GetInvestment(string symbol)
        {
            var investment = _context.Investments.FirstOrDefaultAsync(inv => inv.Symbol == symbol);
            if (investment == null)
                throw new KeyNotFoundException($"Investment with symbol {symbol} was not found in the database");

            return investment;
        }

        public Task<Dictionary<string, Investment>> GetAllInvestmentsAsync()
        {
            if (_context.Investments == null)
                throw new NullReferenceException("Investments collection is null.");

            if (!_context.Investments.Any())
                throw new ArgumentException("Collection does not contain any investments.");

            return _context.Investments.ToDictionaryAsync(investment => investment.Symbol);
        }
    }
}
