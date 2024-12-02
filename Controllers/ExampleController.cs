namespace ExampleApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ExampleApi.Models;
    using ExampleApi.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _service;

        public ExampleController(IExampleService service)
        {
            _service = service;
        }

        // GET api/example
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET api/example/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/example
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExampleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT api/example/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ExampleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.UpdateAsync(id, model);
            return NoContent();
        }

        // DELETE api/example/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
