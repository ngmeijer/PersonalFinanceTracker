using PFT.Models;

namespace PFT.Repositories
{
    public interface IInvestmentRepository
    {
        Task AddInvestmentAsync (Investment investment);
        Task RemoveInvestmentAsync(string symbol);
        Task ChangeInvestmentQuantityAsync(Investment investment);

        Task<Dictionary<string, InvestmentWrapper>> GetAllInvestmentsAsync();
    }
}
