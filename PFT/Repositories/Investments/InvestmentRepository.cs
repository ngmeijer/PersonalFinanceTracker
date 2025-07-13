using Microsoft.AspNetCore.Http.HttpResults;
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

        public void ChangeInvestmentAsync(Investment requestedInvestment)
        {

        }

        public Investment GetInvestment(int id)
        {
            Investment data = _context.Investments.Where(investment => investment.Id == id).First();

            return data;
        }

        public Dictionary<string, Investment> GetAllInvestmentsAsync()
        {
            return _context.Investments.ToDictionary(investment => investment.Symbol);
         }

        public void RemoveInvestmentAsync(int id)
        {
            Investment investment = _context.Investments.Where(current => current.Id == id).First();
            _context.Investments.Remove(investment);
        }

        public void AddInvestmentAsync(Investment investment)
        {
            _context.Investments.Add(investment);
        }
    }
}
