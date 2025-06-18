using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PFT.Data;
using PFT.Models;
using PFT.Services;
using PFT.Utilities;
using System.Net.Http;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Controllers
{
    public class InvestmentsController : Controller
    {
        private IInvestmentService _service;
        private InvestmentsModel _model;

        public InvestmentsController(IInvestmentService service)
        {
            _service = service;
            _model = new();
        }

        public ActionResult Index()
        {
            return View("Investments");
        }

        public async Task<IActionResult> Investments()
        {
             return View();
        }

        public async Task<IActionResult> AddInvestment([FromBody] InvestmentRequest request)
        {
            if (request == null)
            {
                return BadRequest("No data received.");
            }

            var data = await _service.AddInvestmentAsync(request);
            await RefreshData();

            return Ok(new { dataReceived = data });
        }

        [HttpPost]
        public async Task<PartialViewResult> RefreshData()
        {
            await _service.RefreshData();

            return PartialView("_InvestmentTablePartial", _model);
        }
    }
} 