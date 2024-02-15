using Microsoft.AspNetCore.Mvc;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCosts.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CostsController : ControllerBase
    {
        protected readonly ICostApplication _application;

        public CostsController(ICostApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CostResponseViewModel>> Get()
        {
            return Ok(_application.Get()
                ?? Enumerable.Empty<CostResponseViewModel>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CostResponseViewModel>> GetAsync(Guid id)
        {
            var result = await _application.GetAsync(id);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CostRequestViewModel cost)
        {
            var id = await _application.Create(cost);

            return Accepted(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] CostRequestViewModel cost)
        {
            await _application.Update(id, cost);

            return Accepted(cost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _application.Delete(id);

            return Accepted(new { Id = id });
        }
    }
}
