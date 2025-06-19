namespace PFT.Models
{
    public class InvestmentsModel
    {
        public DateTime LatestUpdateTime;
        private Dictionary<string, Investment> _investments = new Dictionary<string, Investment>();
        public InvestmentType InvestmentTypeToAdd;

        public void AddInvestment(string symbol, Investment data)
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

        public Dictionary<string, Investment> GetInvestments() => _investments;

        public float GetPortfolioValue() 
        {
            float portfolioValue = 0;
            foreach(var item in _investments)
            {
                portfolioValue += item.Value.CalculateValue();
            }

            return portfolioValue;
         }
    }

    public enum InvestmentType
    {
        Stock,
        ETF,
        Crypto
    }
}

