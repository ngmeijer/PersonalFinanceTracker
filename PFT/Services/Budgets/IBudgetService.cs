using PFT.Models.Budgets;
using PFT.Models.Investments;
using PFT.Utilities;

namespace PFT.Services.Budgets
{
    public interface IBudgetService
    {
        Task<ServiceResult> AddBudgetAsync(BudgetRequest request);
        Task<ServiceResult> RemoveBudgetAsync(string name);
        Task<ServiceResult> AdjustBudgetAmountAsync(Budget budget);
        Task<Dictionary<string, Budget>> RefreshData();
    }
}
