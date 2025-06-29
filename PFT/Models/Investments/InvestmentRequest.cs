namespace PFT.Models.Investments
{
    /// <summary>
    /// Class containing minimal data, retrieved from the popup modal in the Investments view.
    /// </summary>
    public class InvestmentRequest
    {
        public required string Symbol { get; set; }
        public required int Quantity { get; set; }
        public required int Type { get; set; }
    }
}