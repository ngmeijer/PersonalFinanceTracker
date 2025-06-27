using PFT.Models.Investments;

namespace PFT.Models.Budgets
{
    public class BudgetsModel
    {
        public Dictionary<string, Budget> Budgets = new Dictionary<string, Budget>();
        public Timeframe TimeframeToAdd;
    }

    public enum Timeframe
    {
        Weekly,
        Monthly,
        Yearly,
    }
}

