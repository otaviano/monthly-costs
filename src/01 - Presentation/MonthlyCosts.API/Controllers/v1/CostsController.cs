using Microsoft.AspNetCore.Mvc;
using MonthlyCost.Application.Interfaces;
using MonthlyCost.Application.ViewModels.v1;
using MonthlyCosts.API.Filters;

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
        [ProducesResponseType(typeof(CostResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<IEnumerable<CostResponseViewModel>> Get()
        {
            return Ok(_application.Get()
                ?? Enumerable.Empty<CostResponseViewModel>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CostResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CostResponseViewModel>> GetAsync([FromRoute] Guid id)
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

        public async Task<IActionResult> PostAsync([FromBody] CostRequestViewModel cost)
        {
            var id = await _application.CreateAsync(cost);

            return Accepted($"{Request.Scheme}://{Request.Host}/api/v1/costs/{id}", new  { id } );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CostRequestViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]

        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] CostRequestViewModel cost)
        {
            await _application.UpdateAsync(id, cost);
            
            return Accepted($"{Request.Scheme}://{Request.Host}/api/v1/costs/{id}", new { Id = id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _application.DeleteAsync(id);
           
            return Accepted($"{Request.Scheme}://{Request.Host}/api/v1/costs/{id}");
        }
    }
}
