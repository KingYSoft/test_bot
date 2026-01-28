using Microsoft.AspNetCore.Mvc;
using dotnet_api.Models;

namespace dotnet_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase
{
    private readonly Services.ISampleService _sampleService;
    
    public SampleController(Services.ISampleService sampleService)
    {
        _sampleService = sampleService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SampleModel>>> Get()
    {
        var samples = await _sampleService.GetAllAsync();
        return Ok(samples);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SampleModel>> Get(int id)
    {
        var sample = await _sampleService.GetByIdAsync(id);
        if (sample == null)
        {
            return NotFound();
        }
        return Ok(sample);
    }
    
    [HttpPost]
    public async Task<ActionResult<SampleModel>> Post(SampleModel sample)
    {
        var createdSample = await _sampleService.CreateAsync(sample);
        return CreatedAtAction(nameof(Get), new { id = createdSample.Id }, createdSample);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SampleModel sample)
    {
        var updatedSample = await _sampleService.UpdateAsync(id, sample);
        if (updatedSample == null)
        {
            return NotFound();
        }
        return Ok(updatedSample);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sampleService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}