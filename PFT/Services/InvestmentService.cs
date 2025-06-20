using PFT.Models;
using PFT.Repositories;
using System.Net.Http;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Services
{
    /// <summary>
    /// InvestmentService is meant to retrieve data from APIs - business logic.
    /// </summary>
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
                    Message = $"Request received successfully, but failed to retrieve data. Check if the symbol '{request.Symbol}' is correct."
                };
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = $"Request received successfully, but failed to retrieve data. Check if the symbol '{request.Symbol}' is correct."
                };
            }

            Investment investmentData = new Investment()
            {
                Symbol = data.Symbol,
                Quantity = request.Quantity,
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

        /// <summary>
        /// This is the only method where the data retrieved from TwelveData's stock API is actually used. Any time the Refresh button is clicked or an investment is added, the full table is updated.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, InvestmentWrapper>> RefreshData()
        {
            Dictionary<string, InvestmentWrapper> investmentsCollection = await _repository.GetAllInvestmentsAsync();
            foreach (KeyValuePair<string, InvestmentWrapper> entry in investmentsCollection)
            {
                entry.Value.StockData = await RequestStockData(entry.Key);
            }

            return investmentsCollection;
        }

        public Task<ServiceResult> RemoveInvestmentAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> AdjustInvestmentQuantityAsync(Investment request)
        {
            TwelveDataQuote data = await RequestStockData(request.Symbol);
            if (data == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = $"Request received successfully, but failed to retrieve data. Check if the symbol '{request.Symbol}' is correct."
                };
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = $"Request received successfully, but failed to retrieve data. Check if the symbol '{request.Symbol}' is correct."
                };
            }

            Investment investmentData = new Investment()
            {
                Symbol = data.Symbol,
                Quantity = request.Quantity,
                Type = (InvestmentType)request.Type
            };

            await _repository.ChangeInvestmentQuantityAsync(investmentData);
            return new ServiceResult
            {
                Success = true,
                Message = "Investment quantity changed successfully"
            };
        }
    }
}
