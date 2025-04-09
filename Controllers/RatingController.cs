using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTO;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly RatingService _service;

    public RatingController(RatingService service)
    {
        _service = service;
    }

    [HttpGet("session/{sessionId}")]
    public async Task<IActionResult> GetBySession(int sessionId)
    {
        var ratings = await _service.GetBySessionIdAsync(sessionId);
        return Ok(ratings);
    }

    [HttpGet("participant/{participantId}")]
    public async Task<IActionResult> GetByParticipant(int participantId)
    {
        var ratings = await _service.GetByParticipantIdAsync(participantId);
        return Ok(ratings);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RatingDto dto)
    {
        var rating = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetBySession), new { sessionId = rating.SessionId }, rating);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}