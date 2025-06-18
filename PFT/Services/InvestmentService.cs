using PFT.Models;
using PFT.Repositories;
using System.Net.Http;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Services
{
    public class InvestmentService : IInvestmentService
    {
        private IInvestmentRepository _repository;
        private HttpClient _httpClient;
        private ITwelveDataClient _twelveDataClient;

        public InvestmentService(IInvestmentRepository repo) 
        {
            _repository = repo;
            _httpClient = new HttpClient();

            string apiKey = Utilities.Utilities.ReadFromFile("apikey.txt");
            _twelveDataClient = new TwelveDataClient(apiKey, _httpClient);
        }

        public async Task<ServiceResult> AddInvestmentAsync(InvestmentRequest request)
        {
            TwelveDataQuote data = await RequestStockData(request.Symbol);
            if (data == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Failed to retrieve data"
                };
            }

            Investment investmentData = new Investment()
            {
                Symbol = data.Symbol,
                AmountHeld = request.Quantity,
                Type = (InvestmentType)request.Type
            };
            await _repository.AddInvestmentAsync(investmentData);
            return new ServiceResult
            {
                Success = true,
                Message = "Investment added successfully"
            };
        }

        public async Task<TwelveDataQuote> RequestStockData(string symbol)
        {
            try
            {
                TwelveDataQuote stockData = await _twelveDataClient.GetQuoteAsync(symbol, "5min");
                return stockData;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return null;
            }
        }

        public async Task<ServiceResult> RefreshData()
        {
            //Dictionary<string, Investment> investmentsCollection = _repository.GetInvestments();
            //foreach (KeyValuePair<string, Investment> entry in investmentsCollection)
            //{
            //    entry.Value.StockData = await RequestStockData(entry.Key);
            //    entry.Value.CalculateValue();
            //}

            return new ServiceResult
            {
                Success = true,
                Message = "Successfully refreshed investment data"
            };
        }
    }
}
