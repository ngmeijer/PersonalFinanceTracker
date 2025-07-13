using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFT.Models.Investments;
using PFT.Services.Investments;

namespace PFT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentsApiController : ControllerBase
    {
        private IInvestmentService _service;

        public InvestmentsApiController(IInvestmentService service) 
        {
            _service = service;
        }

        //Get investment
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvestment(int id)
        {
            InvestmentWrapper data = await _service.GetInvestment(id);

            return Ok(data);
        }

        //get investments
        [HttpGet]
        public async Task<IActionResult> GetInvestments()
        {
            return Ok(_service.GetInvestments());
        }

        //Add investment
        [HttpPost]
        public async Task<IActionResult> AddInvestment(InvestmentRequest investment)
        {
            await _service.AddInvestmentAsync(investment);

            return Ok(investment);
        }

        //delete investments
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInvestment(int id)
        {
            await _service.DeleteInvestment(id);

            return Ok();
        }

        // Change investment
        [HttpPatch]
        public async Task<IActionResult> ChangeInvestment()
        {
            return null;
        }
    }
}
