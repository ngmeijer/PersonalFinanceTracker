using PFT.Models;

namespace PFT.Services
{
    public interface IInvestmentService
    {
        Task<ServiceResult> AddInvestmentAsync(InvestmentRequest request);
        Task<ServiceResult> RemoveInvestmentAsync(string symbol);
        Task<ServiceResult> AdjustInvestmentQuantityAsync(Investment investment);
        Task<Dictionary<string, InvestmentWrapper>> RefreshData();
     }
}
