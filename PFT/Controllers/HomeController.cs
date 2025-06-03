using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFT.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpClient;
        private ITwelveDataClient _twelveDataClient;
        private StreamReader _reader;

        private InvestmentsModel _investmentsModel;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();

            string apiKey = ReadFromFile("apikey.txt");
            _twelveDataClient = new TwelveDataClient(apiKey, _httpClient);

            _investmentsModel = new();
        }

        private string ReadFromFile(string fileName)
        {
            try
            {
                using StreamReader reader = new(fileName);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
                return text;
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Investments()
        {
            await RequestStockData("NVDA");
            await RequestStockData("AAPL");
            await RequestStockData("AMZN");

            _investmentsModel.LatestUpdateTime = DateTime.Now;
            return View(_investmentsModel);
        }

        public async Task RequestStockData(string symbol)
        {
            TwelveDataTimeSeries data = await _twelveDataClient.GetTimeSeriesAsync(symbol, "30min");
            _investmentsModel.AddInvestment(data.Symbol, data);
        }

        public IActionResult Transactions()
        {
            return View();
        }

        public IActionResult Budgets()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

public class API_Obj
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string currency { get; set; }
    public string exchange {  get; set; }
    public string country { get; set; }
    public string type { get; set; }
}

public class TimeSeries
{
    public Dictionary<string, string> meta { get; set; }
    public IList<Dictionary<string, string>> values { get; set; }
    public string status { get; set; }
}