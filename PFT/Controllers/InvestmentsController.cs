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
        private IInvestmentService? _service;
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
            await RefreshData();

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvestment([FromBody] InvestmentRequest request)
        {
            if (request == null)
            {
                return BadRequest("No data received.");
            }

            try
            {
                var result = await _service.AddInvestmentAsync(request);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                await RefreshData();

                return Ok(new { dataReceived = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured here: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveInvestment(string symbol)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AdjustQuantityOfInvestment(Investment request) 
        {
            if (request == null)
            {
                return BadRequest("No data received.");
            }

            try
            {
                var result = await _service.AdjustInvestmentQuantityAsync(request);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                await RefreshData();

                return Ok(new { dataReceived = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured here: {ex.Message}");
            }
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