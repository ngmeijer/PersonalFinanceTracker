using PFT.Models.Investments;

namespace PFT.Repositories.Investments
{
    public interface IInvestmentRepository
    {
        Task AddInvestmentAsync(Investment investment);
        Task RemoveInvestmentAsync(string symbol);
        Task ChangeInvestmentAsync(Investment investment);

        Task<Dictionary<string, Investment>> GetAllInvestmentsAsync();
        Task<bool> CheckIfInvestmentExists(string symbol);
    }
}
