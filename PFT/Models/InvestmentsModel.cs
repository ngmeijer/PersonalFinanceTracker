namespace PFT.Models
{
    public class InvestmentsModel
    {
        public DateTime LatestUpdateTime;
        public Dictionary<string, InvestmentWrapper> Investments = new Dictionary<string, InvestmentWrapper>();
        public InvestmentType InvestmentTypeToAdd;

        public Dictionary<string, InvestmentWrapper> GetInvestments() => Investments;

        public float GetPortfolioValue() 
        {
            float portfolioValue = 0;
            foreach(var item in Investments)
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

