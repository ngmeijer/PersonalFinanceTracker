using Microsoft.AspNetCore.Mvc;
using PFT.Models;

namespace PFT.Controllers
{
    public class BudgetsController : Controller
    {
        private BudgetsModel _model;

        public BudgetsController()
        {
            _model = new();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Budgets()
        {
            return View(_model);
        }
    }
}
