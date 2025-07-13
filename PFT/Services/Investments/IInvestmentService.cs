using Microsoft.AspNetCore.Mvc;
using PFT.Models.Investments;
using PFT.Utilities;

namespace PFT.Services.Investments
{
    public interface IInvestmentService
    {
        Task<ServiceResult> AddInvestmentAsync(InvestmentRequest request);
        Task<ServiceResult> DeleteInvestment(int id);
        Task<ServiceResult> ChangeInvestmentAsync(Investment investment);
        Task<Dictionary<string, InvestmentWrapper>> GetInvestments();
        Task<InvestmentWrapper> GetInvestment(int id);
    }
}
