using PFT.Models.Budgets;
using PFT.Models.Investments;
using PFT.Repositories.Budgets;
using PFT.Utilities;

namespace PFT.Services.Budgets
{
    public class BudgetService : IBudgetService
    {
        private IBudgetRepository _repository;

        public BudgetService(IBudgetRepository repo)
        {
            _repository = repo;
        }

        public async Task<ServiceResult> AddBudgetAsync(BudgetRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "A budget should have a name.",
                };
            }

            Budget budgetData = new Budget()
            {
                Name = request.Name,
                Amount = request.Amount,
            };

            await _repository.AddBudgetAsync(budgetData);
            return new ServiceResult
            {
                Success = true,
                Message = "Budget added successfully"
            };
        }

        public Task<ServiceResult> AdjustBudgetAmountAsync(Budget budget)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, InvestmentWrapper>> RefreshData()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> RemoveBudgetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
