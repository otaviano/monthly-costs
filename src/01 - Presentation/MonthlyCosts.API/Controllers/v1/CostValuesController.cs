using Microsoft.AspNetCore.Mvc;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;

namespace MonthlyCosts.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CostValuesController : ControllerBase
    {
        protected readonly ICostApplication _application;

        public CostValuesController(ICostApplication application)
        {
            _application = application;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CostValueViewModel>> Get()
        {
            return Ok();
            //_application.Get()
            //    ?? Enumerable.Empty<CostValueViewModel>());
        }

        [HttpGet("{id}")]
        public ActionResult<CostValueViewModel> Get(Guid id)
        {
            var result = _application.GetAsync(id);
            if (result is null) return NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CostRequestViewModel cost)
        {
            var id = await _application.CreateAsync(cost);

            return Accepted(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CostValueViewModel cost)
        {
            //await _application.Update(cost);

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _application.DeleteAsync(id);

            return Accepted();
        }
    }
}
