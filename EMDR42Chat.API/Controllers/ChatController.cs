using EMDR42Chat.Domain.Commons.DTO;
using EMDR42Chat.Infrastructure.Services.Implementations;
using EMDR42Chat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EMDR42Chat.API.Controllers;

[Route("chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IRedisService _service;
    public ChatController(IRedisService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Create(ChatDataDTO request)
    {
        try
        {
            string json = System.Text.Json.JsonSerializer.Serialize(request);
            await _service.SetValueAsync("json", json);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<ChatDataDTO>> Get()
    {
        string json = await _service.GetValueAsync("json");
        var responseObject = System.Text.Json.JsonSerializer.Deserialize<ChatDataDTO>(json);

        return Ok(responseObject);
    }
}
