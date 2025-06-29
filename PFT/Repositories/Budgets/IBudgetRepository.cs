using PFT.Models.Budgets;
using PFT.Models.Investments;
using PFT.Utilities;

namespace PFT.Repositories.Budgets
{
    public interface IBudgetRepository
    {
        Task<ServiceResult> AddBudgetAsync(Budget investment);
        Task<ServiceResult> RemoveBudgetAsync(string symbol);
        Task<ServiceResult> ChangeBudgetAmountAsync(Budget budget);

        Task<Dictionary<string, Budget>> GetAllBudgetsAsync();
    }
}
