using PFT.Models;

namespace PFT.Services
{
    public interface IInvestmentService
    {
        Task<ServiceResult> AddInvestmentAsync(InvestmentRequest request);
        Task<Dictionary<string, InvestmentWrapper>> RefreshData();
     }
}
