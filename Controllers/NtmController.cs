using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NtmController: ControllerBase
{
    private readonly ILogger<NtmController> _logger;

    public NtmController(ILogger<NtmController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "niktamer")]
    public String Niktamer()
    {
        return "yo bro";
    }
    
    [HttpGet(Name = "test")]
    public String Test()
    {
        return "yo bro2348";
    }
}