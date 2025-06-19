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
            return View("Investments", _model);
        }

        public async Task<IActionResult> Investments()
        {
            return View(_model);
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
            _model.Investments = await _service.RefreshData();
            
            _model.LatestUpdateTime = DateTime.Now;

            return PartialView("_InvestmentTablePartial", _model);
        }
    }
}