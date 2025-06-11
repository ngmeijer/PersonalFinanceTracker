using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Models
{
    public class InvestmentsModel
    {
        public DateTime LatestUpdateTime;
        private Dictionary<string, InvestmentData> _investments = new Dictionary<string, InvestmentData>();

        public void AddInvestment(string symbol, InvestmentData data)
        {
            if (symbol == null)
            {
                return;
            }

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
        public TwelveDataQuote stockData { get; set; }
        public float CurrentValue { get; set; }
    }
}

