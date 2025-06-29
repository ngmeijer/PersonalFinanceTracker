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

        public int GetTotalBudgetValue()
        {
            int value = 0;
            foreach(var item in Budgets)
            {
                switch (item.Value.Interval)
                {
                    case Timeframe.Weekly:
                        value += item.Value.MaxAmount * 52;
                        break;
                    case Timeframe.Monthly:
                        value += item.Value.MaxAmount * 12;
                        break;
                    case Timeframe.Yearly:
                        value += item.Value.MaxAmount;
                        break;
                }
            }

            return value;
        }
    }

    public enum Timeframe
    {
        Weekly,
        Monthly,
        Yearly,
    }
}

