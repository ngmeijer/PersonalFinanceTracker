using Microsoft.AspNetCore.Mvc;
using PFT.Models.Budgets;
using PFT.Services.Budgets;

namespace PFT.Controllers
{
    public class BudgetsController : Controller
    {
        private BudgetsModel _model;
        private IBudgetService _service;

        public BudgetsController(IBudgetService service)
        {
            _service = service;
            _model = new();
        }

        public IActionResult Index()
        {
            return View("Budgets", _model);
        }

        public async Task<IActionResult> Budgets()
        {
            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] BudgetRequest request)
        {
            if(request == null)
            {
                return BadRequest("No data received.");
            }

            try
            {
                var result = await _service.AddBudgetAsync(request);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                return Ok(new { dataReceived = result.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured here: {ex.Message}");
            }
        }
    }
}
