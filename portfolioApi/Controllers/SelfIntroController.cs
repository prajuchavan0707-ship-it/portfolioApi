using Microsoft.AspNetCore.Mvc;

namespace portfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SelfIntroController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new{ message = "Hello, My self Prajakta Chavan and I am 3rd year AI&DS Student in DKTE college."});
    }
}