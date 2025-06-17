using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Models
{
    public class InvestmentsModel
    {
        public DateTime LatestUpdateTime;
        private Dictionary<string, InvestmentData> _investments = new Dictionary<string, InvestmentData>();
        public InvestmentType InvestmentTypeToAdd;

        public void AddInvestment(string symbol, InvestmentData data)
        {
            if (symbol == null)
            {
                return;
            }

            data.CurrentValue = (float)(data.AmountHeld * data.StockData.Close);

            if (_investments.ContainsKey(symbol))
            {
                _investments[symbol] = data;
                return;
            }

            _investments.Add(symbol, data);
        }

        public Dictionary<string, InvestmentData> GetInvestments() => _investments;

        public float GetPortfolioValue() 
        {
            float portfolioValue = 0;
            foreach(var item in _investments)
            {
                portfolioValue += item.Value.CurrentValue;
            }

            return portfolioValue;
         }
    }

    public class InvestmentData
    {
        public TwelveDataQuote StockData { get; set; }
        public InvestmentType Type;
        public float AmountHeld { get; set; }
        public float CurrentValue { get; set; }


        public void CalculateValue()
        {
            CurrentValue = (float)(AmountHeld * StockData.Close);
        }
    }

    public enum InvestmentType
    {
        Stock,
        ETF,
        Crypto
    }
}

