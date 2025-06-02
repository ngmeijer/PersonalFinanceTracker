using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFT.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace PFT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                String URLString = "https://v6.exchangerate-api.com/v6/3c450a04af4133fe0ab4fa07/latest/USD";
                using (_httpClient)
                {
                    var response = await _httpClient.GetStringAsync(URLString);
                    API_Obj Test = JsonConvert.DeserializeObject<API_Obj>(response);
                }
            }
            catch (Exception ex)
            {
                   Console.Write(ex.Message);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Investments()
        {
            return View();
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
    public string time_last_update_utc { get; set; }
    public string base_code { get; set; }
    public ConversionRate conversion_rates { get; set; }
}

public class ConversionRate
{
    public double EUR { get; set; }
    public double USD { get; set; }
}