using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Models.Investments
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
    }

    public class InvestmentWrapper
    {
        public Investment CachedData { get; set; }
        public TwelveDataQuote StockData { get; set; }

        public float PercentageOfTotalValue { get; set; }

        public float CalculateValue()
        {
            return (float)(CachedData.Quantity * StockData.Close);
        }

        public float CalculatePercentageOfTotalValue(float totalValue)
        {
            return CalculateValue() / totalValue;
        }

        public float CalculatePercentageOfTotalStockAmount(float totalStockCount)
        {
            return CalculateValue() / totalStockCount;
        }
    }
}

