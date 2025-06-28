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
            await RefreshData();

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] BudgetRequest request)
        {
            if(request == null)
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);

                    return BadRequest(string.Join(" | ", errors));
                }
            }

            try
            {
                var result = await _service.AddBudgetAsync(request);
                if (!result.Success)
                {
                    return BadRequest(result.Message);
                }

                await RefreshData();

                return Ok(new { dataReceived = result.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An unexpected error occured here: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<PartialViewResult> RefreshData()
        {
            _model.SetAllBudgetData(await _service.RefreshData());

            return PartialView("_BudgetsViewPartial", _model);
        }
    }
}
