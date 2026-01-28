using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { 
            message = "Hello from .NET 8.0 API!", 
            timestamp = DateTime.UtcNow,
            framework = ".NET 8.0",
            frontend = "Vue 3 + Vuetify 3 + TypeScript"
        });
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(new { 
            id = id,
            message = $"Item with ID {id}",
            timestamp = DateTime.UtcNow
        });
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] object data)
    {
        return Ok(new { 
            message = "Data received successfully",
            receivedData = data,
            timestamp = DateTime.UtcNow
        });
    }
}