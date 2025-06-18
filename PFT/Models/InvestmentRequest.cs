namespace PFT.Models
{
    /// <summary>
    /// Class containing minimal data, retrieved from the popup modal in the Investments view.
    /// </summary>
    public class InvestmentRequest
    {
        public required string Symbol { get; set; }
        public required int Quantity { get; set; }
        public int Type { get; set; }
    }

    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

