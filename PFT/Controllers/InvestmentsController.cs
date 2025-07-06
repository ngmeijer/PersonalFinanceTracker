using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PFT.Data;
using PFT.Models.Investments;
using PFT.Repositories.Investments;
using PFT.Services.Investments;
using PFT.Utilities;
using System.Net.Http;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;
using TwelveDataSharp.Library.ResponseModels;

namespace PFT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentsController : Controller
    {
        private IInvestmentService? _service;
        private InvestmentsModel _model;

        public InvestmentsController(IInvestmentService service)
        {
            _service = service;
            _model = new();
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetInvestment(string symbol)
        {
            InvestmentWrapper data = await _service.GetInvestment(symbol);

            return Ok(data);
        }

        public async Task<IActionResult> Investments()
        {
            await RefreshData();

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvestment(InvestmentRequest request)
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
        public async Task<IActionResult> RemoveInvestment([FromBody]string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                return BadRequest("No data received");
            }

            try
            {
                var result = await _service.RemoveInvestmentAsync(symbol);
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

        [HttpPut]
        public async Task<IActionResult> ChangeInvestment([FromBody]Investment request) 
        {
            if (request == null)
            {
                return BadRequest("No data received.");
            }

            try
            {
                var result = await _service.ChangeInvestmentAsync(request);
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