using System.Diagnostics;
using Json.More;
using Microsoft.AspNetCore.Mvc;
using RhythmAI.Models;

namespace RhythmAI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IChatService _chatService;
    public HomeController(ILogger<HomeController> logger, IChatService chatService)
    {
        _logger = logger;
        _chatService = chatService;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpPost("chat")]
    public async Task<ActionResult<ChatModel>> SendMessage([FromBody] ChatModel message)
    {
        if (message == null)
        {
            return BadRequest("Invalid request body");
        }
        var result = await _chatService.SendMessage(message);
        var resultChat = new ChatModel { message = result };
        return Ok(resultChat);

    }
}
