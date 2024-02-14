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
        public ActionResult<IEnumerable<CostViewModel>> Get()
        {
            return Ok(_application.Get()
                ?? Enumerable.Empty<CostViewModel>());
        }

        [HttpGet("{id}")]
        public ActionResult<CostViewModel> Get(Guid id)
        {
            var result = _application.Get(id);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CostViewModel cost)
        {
            var id = await _application.Create(cost);

            return Accepted(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CostViewModel cost)
        {
            cost.Id = id;
            await _application.Update(cost);

            return Accepted(cost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _application.Delete(id);

            return Accepted(id);
        }
    }
}
