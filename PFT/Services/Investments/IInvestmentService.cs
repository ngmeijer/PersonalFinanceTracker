using PFT.Models.Investments;
using PFT.Utilities;

namespace PFT.Services.Investments
{
    public interface IInvestmentService
    {
        Task<ServiceResult> AddInvestmentAsync(InvestmentRequest request);
        Task<ServiceResult> RemoveInvestmentAsync(string symbol);
        Task<ServiceResult> ChangeInvestmentAsync(Investment investment);
        Task<Dictionary<string, InvestmentWrapper>> RefreshData();
    }
}
