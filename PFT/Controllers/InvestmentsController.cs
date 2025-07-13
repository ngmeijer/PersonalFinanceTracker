using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PFT.Data;
using PFT.Models;
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
    public class InvestmentsController : Controller
    {
        private IInvestmentService? _service;
        private InvestmentsModel _model;
        private DashboardModel _summaryModel;

        public InvestmentsController(IInvestmentService service)
        {
            _service = service;
            _model = new();
        }
      
        public async Task<IActionResult> Investments()
        {
            await RefreshData();

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestment(int id)
        {
            InvestmentWrapper data = await _service.GetInvestment(id);

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestments()
        {
            return Ok();
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
                    return BadRequest(result.Messages[0]);
                }

                await RefreshData();

                return Ok("Added investment succesfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured here: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveInvestment(int id)
        {
            try
            {
                var result = await _service.DeleteInvestment(id);
                if (!result.Success)
                {
                    return BadRequest(result.GetMessages());
                }

                await RefreshData();

                return Ok("Removed investment succesfully.");
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
                    return BadRequest(result.GetMessages());
                }

                await RefreshData();

                return Ok("Changed investment succesfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured here: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> RefreshData()
        {
            _model.Investments = await _service.GetInvestments();
            
            _model.LatestUpdateTime = DateTime.Now;

            return PartialView("_InvestmentTablePartial", _model);
        }
    }
}