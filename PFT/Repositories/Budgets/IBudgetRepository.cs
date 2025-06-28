using PFT.Models.Budgets;
using PFT.Models.Investments;
using PFT.Utilities;

namespace PFT.Repositories.Budgets
{
    public interface IBudgetRepository
    {
        Task<ServiceResult> AddBudgetAsync(Budget investment);
        Task RemoveBudgetAsync(string symbol);
        Task ChangeBudgetAmountAsync(Budget budget);

        Task<Dictionary<string, Budget>> GetAllBudgetsAsync();
    }
}
