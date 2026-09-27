using Microsoft.AspNetCore.Mvc;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]  // 
public class HelloWorldController : ControllerBase //for getting method like get post so that's why we inherit 
{
    // GET: api/helloworld
    [HttpGet]
    public IActionResult Get() //code and data send to server
    {
        return Ok(new { message = "Hello, World!" });
    }
}
