namespace PFT.Models.Budgets
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Timeframe Interval { get; set; }
        public int Amount { get; set; }
    }
}
