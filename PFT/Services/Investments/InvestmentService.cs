using Microsoft.AspNetCore.Mvc;
using PFT.Models.Investments;
using PFT.Repositories.Investments;
using PFT.Utilities;
using System.Net.Http;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Services.Investments
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

            TwelveDataQuote data = await RequestRealtimeStockData(request.Symbol);
            if (data == null)
            {
                throw new ArgumentException($"Provided quantity ({request.Quantity}) is less than the minimum (1)");
            }

            //bool exists = await _repository.CheckIfInvestmentExists(request.Symbol);
            //if(exists)
            //{
            //    throw new ArgumentException($"Investment with symbol '{request.Symbol} already exists in the database.'");
            //}

            if (data == null)
            {
                throw new ArgumentException("Data received from API is null.");
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                throw new ArgumentException($"Invalid data received from API.");
            }

            Investment investmentData = new Investment()
            {
                Symbol = data.Symbol,
                Quantity = request.Quantity,
                Type = (InvestmentType)request.Type
            };

            _repository.AddInvestmentAsync(investmentData);
            return new ServiceResult
            {
                Success = true,
                Messages = { "Investment added successfully" }
            };
        }

        public async Task<TwelveDataQuote> RequestRealtimeStockData(string symbol)
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
        public async Task<Dictionary<string, InvestmentWrapper>> GetInvestments()
        {
            Dictionary<string, Investment> investmentsCollection = _repository.GetAllInvestmentsAsync();

            Dictionary<string, InvestmentWrapper> completeData = new();
            foreach (KeyValuePair<string, Investment> entry in investmentsCollection)
            {
                completeData.Add(entry.Key, new InvestmentWrapper
                {
                    CachedData = entry.Value,
                    StockData = await RequestRealtimeStockData(entry.Value.Symbol)
                });
            }

            return completeData;
        }

        public async Task<ServiceResult> DeleteInvestment(int id)
        {
            try
            {
                _repository.RemoveInvestmentAsync(id);
            }
            catch (Exception e)
            {
                return new ServiceResult
                {
                    Success = false,
                    Messages =
                    {
                        $"Failed to add investment: {e.Message}"
                    }
                };
            }

            return new ServiceResult
            {
                Success = true,
                Messages = { "Investment removed successfully" }
            };
        }

        public async Task<ServiceResult> ChangeInvestmentAsync(Investment request)
        {
            TwelveDataQuote data = await RequestRealtimeStockData(request.Symbol);
            if (data.ResponseStatus != Enums.TwelveDataClientResponseStatus.Ok)
            {
                return new ServiceResult
                {
                    Success = false,
                    Messages = { $"API request failed. Reason: {data.ResponseMessage}." }
                };
            }

            if (data == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Messages = { $"Request received successfully, but failed to retrieve data. Check if the symbol '{request.Symbol}' is correct." }
                };
            }

            if (string.IsNullOrEmpty(data.Name))
            {
                return new ServiceResult
                {
                    Success = false,
                    Messages = { $"Request received successfully, but failed to retrieve data. Check if the symbol '{request.Symbol}' is correct." }
                };
            }

            Investment investmentData = new Investment()
            {
                Symbol = data.Symbol,
                Quantity = request.Quantity,
                Type = request.Type
            };

            _repository.ChangeInvestmentAsync(investmentData);
            return new ServiceResult
            {
                Success = true,
                Messages = { "Investment quantity changed successfully" }
            };
        }

        public async Task<InvestmentWrapper> GetInvestment(int id)
        {
            Investment cachedData = _repository.GetInvestment(id);

            TwelveDataQuote realtimeData = await RequestRealtimeStockData(cachedData.Symbol);

            return new InvestmentWrapper
            {
                CachedData = cachedData,
                StockData = realtimeData,
            };
        }
    }
}
