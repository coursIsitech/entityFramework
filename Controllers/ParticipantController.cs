using Microsoft.AspNetCore.Mvc;
using MyWebApi.DTO;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly ParticipantService _service;

    public ParticipantController(ParticipantService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var participants = await _service.GetAllAsync();
        return Ok(participants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var participant = await _service.GetByIdAsync(id);
        if (participant == null)
            return NotFound();

        return Ok(participant);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ParticipantDto dto)
    {
        var participant = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = participant.Id }, participant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ParticipantDto dto)
    {
        if (id != dto.Id)
            return BadRequest();

        var updated = await _service.UpdateAsync(dto);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}