using Microsoft.AspNetCore.Mvc;
using PFT.Models;
using PFT.Utilities;
using System.Net.Http;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Controllers
{
    public class InvestmentsController : Controller
    {
        private InvestmentsModel _investmentsModel;
        private HttpClient _httpClient;
        private ITwelveDataClient _twelveDataClient;

        public InvestmentsController()
        {
            _investmentsModel = new();
            _httpClient = new HttpClient();

            string apiKey = Utilities.Utilities.ReadFromFile("apikey.txt");
            _twelveDataClient = new TwelveDataClient(apiKey, _httpClient);
        }

        public ActionResult Index()
        {
            return View("Investments");
        }

        public async Task<IActionResult> Investments()
        {
            await RequestStockData("NVDA");
            await RequestStockData("AAPL");

            _investmentsModel.LatestUpdateTime = DateTime.Now;
            return View(_investmentsModel);
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

        public async void AddInvestment(string symbol)
        {
            TwelveDataQuote stockData = await RequestStockData(symbol);
            InvestmentData investmentData = new InvestmentData();
            investmentData.CurrentValue = 5000;
            investmentData.stockData = stockData;
            _investmentsModel.AddInvestment(stockData.Symbol, investmentData);
        }
    }
}
