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
        public HomeController()
        {

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
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