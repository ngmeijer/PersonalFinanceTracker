namespace PFT.Models.Budgets
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Timeframe Interval { get; set; }
        public int MaxAmount { get; set; }
        public int CurrentlySpent { get ; set; }

        public float CalculatePercentageSpent()
        {
            return (float)CurrentlySpent / MaxAmount;
        }
    }
}
