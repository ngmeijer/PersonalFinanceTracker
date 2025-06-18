using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Models
{
    /// <summary>
    /// Investment model containing all data needed to display in the View. Data is retrieved from the TwelveData stock API.
    /// </summary>
    public class Investment
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public InvestmentType Type;
        public float AmountHeld { get; set; }

        public float CalculateValue(float currentPrice)
        {
            return (float)(AmountHeld * currentPrice);
        }
    }
}

