using Microsoft.AspNetCore.Mvc;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.API.Filters;

namespace MonthlyCosts.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CostValuesController : ControllerBase
    {
        protected readonly ICostValueApplication _application;

        public CostValuesController(ICostValueApplication application)
        {
            _application = application;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CostValueResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<IEnumerable<CostValueResponseViewModel>> Get()
        {
            return Ok(_application.Get() 
                ?? Enumerable.Empty<CostValueResponseViewModel>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CostValueResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CostValueResponseViewModel>> GetAsync([FromRoute] Guid id)
        {
            var result = await _application.GetAsync(id);
            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]

        public async Task<IActionResult> PostAsync([FromBody] CostValueRequestViewModel cost)
        {
            var id = await _application.CreateAsync(cost);

            return Accepted(new { Id = id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CostRequestViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]

        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] CostValueRequestViewModel cost)
        {
            await _application.UpdateAsync(id, cost);

            return Accepted();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _application.DeleteAsync(id);

            return Accepted();
        }

        [HttpGet("{id}/sum")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CostValueSumResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CostValueSumResponseViewModel>> SumAsync([FromRoute] Guid id)
        {
            var total = await _application.SumAsync(id);

            return Ok(new { total });
        }
    }
}
