namespace PFT.Models.Budgets
{
    public class BudgetRequest
        {
            public required string Name { get; set; }
            public required int Amount { get; set; }
            public required int Timeframe { get; set; }
    }
}
