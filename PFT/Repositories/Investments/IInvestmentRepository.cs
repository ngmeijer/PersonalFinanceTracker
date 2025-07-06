using PFT.Models.Investments;

namespace PFT.Repositories.Investments
{
    public interface IInvestmentRepository
    {
        Task AddInvestmentAsync(Investment investment);
        Task RemoveInvestmentAsync(string symbol);
        Task ChangeInvestmentAsync(Investment investment);
        Task<Investment> GetInvestment(string symbol);
        Task<Dictionary<string, Investment>> GetAllInvestmentsAsync();
    }
}
