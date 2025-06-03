using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Models
{
    public class InvestmentsModel
    {
        public DateTime LatestUpdateTime;
        private Dictionary<string, TwelveDataTimeSeries> _investments = new Dictionary<string, TwelveDataTimeSeries>();

        public void AddInvestment(string symbol, TwelveDataTimeSeries data)
        {
            if(_investments.ContainsKey(symbol))
            {
                _investments[symbol] = data;
                return;
            }

            _investments.Add(symbol, data);
        }

        public Dictionary<string, TwelveDataTimeSeries> GetInvestments() => _investments;
    }
}
