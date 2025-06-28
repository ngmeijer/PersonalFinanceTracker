using PFT.Models.Investments;

namespace PFT.Models.Budgets
{
    public class BudgetsModel
    {
        private Dictionary<string, Budget> Budgets = new Dictionary<string, Budget>();
        public Timeframe TimeframeToAdd;

        public Dictionary<string, Budget> GetBudgets() => Budgets;

        public void SetAllBudgetData(Dictionary<string, Budget> newData)
        {
            Budgets = newData;
        }
    }

    public enum Timeframe
    {
        Weekly,
        Monthly,
        Yearly,
    }
}

