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
        public InvestmentType Type { get; set; }
        public float Quantity { get; set; }

        public float CalculateValue()
        {
            return (float)(Quantity * 1);
        }
    }
}

