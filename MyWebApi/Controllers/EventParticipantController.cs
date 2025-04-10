using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTO;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventParticipantController : ControllerBase
{
    private readonly EventParticipantService _service;

    public EventParticipantController(EventParticipantService service)
    {
        _service = service;
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetByEvent(int eventId)
    {
        var list = await _service.GetByEventIdAsync(eventId);
        return Ok(list);
    }

    [HttpGet("participant/{participantId}")]
    public async Task<IActionResult> GetByParticipant(int participantId)
    {
        var list = await _service.GetByParticipantIdAsync(participantId);
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventParticipantDto dto)
    {
        var result = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetByEvent), new { eventId = result.EventId }, result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int eventId, [FromQuery] int participantId)
    {
        var deleted = await _service.DeleteAsync(eventId, participantId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}