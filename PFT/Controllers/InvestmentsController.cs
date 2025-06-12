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
            await AddInvestment("NVDA");
            await AddInvestment("AAPL");

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

        public async Task AddInvestment(string symbol)
        {
            TwelveDataQuote data = await RequestStockData(symbol);
            InvestmentData investmentData = new InvestmentData()
            {
                AmountHeld = 5,
                stockData = data
            };
            investmentData.CalculateValue();
            _investmentsModel.AddInvestment(data.Symbol, investmentData);
        }

        [HttpPost]
        public async Task<PartialViewResult> RefreshData()
        {
            if (_investmentsModel == null)
                _investmentsModel = new();

            //TODO Connect to database to retrieve the investments held. AddInvestments calls are placeholders
            if (_investmentsModel.GetInvestments().Count == 0)
            {
                await AddInvestment("NVDA");
                await AddInvestment("AAPL");
            }

            Dictionary<string, InvestmentData> investmentsCollection = _investmentsModel.GetInvestments();
            foreach(KeyValuePair<string, InvestmentData> entry in investmentsCollection)
            {
                entry.Value.stockData = await RequestStockData(entry.Key);
                entry.Value.CalculateValue();
            }
            _investmentsModel.LatestUpdateTime = DateTime.Now; 

            return PartialView("_InvestmentTablePartial", _investmentsModel);
        }

        public PartialViewResult GetInvestmentTable()
        {
            var data = _investmentsModel.GetInvestments();
            return PartialView("_InvestmentTablePartial", data);
        }
    }
} 