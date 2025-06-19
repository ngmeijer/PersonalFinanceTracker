using PFT.Models;

namespace PFT.Repositories
{
    public interface IInvestmentRepository
    {
        Task AddInvestmentAsync (Investment investment);

        Task<Dictionary<string, Investment>> GetAllInvestmentsAsync();
    }
}
