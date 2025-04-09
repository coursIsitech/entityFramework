using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class SessionService
{
    private readonly ISessionRepository _repository;

    public SessionService(ISessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SessionDto>> GetAllAsync()
    {
        var sessions = await _repository.GetAllAsync();
        return sessions.Select(s => new SessionDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            EventId = s.EventId,
            RoomId = s.RoomId
        });
    }

    public async Task<SessionDto?> GetByIdAsync(int id)
    {
        var s = await _repository.GetByIdAsync(id);
        if (s == null) return null;

        return new SessionDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            EventId = s.EventId,
            RoomId = s.RoomId
        };
    }

    public async Task<SessionDto> AddAsync(SessionDto dto)
    {
        var session = new Session
        {
            Title = dto.Title,
            Description = dto.Description,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            EventId = dto.EventId,
            RoomId = dto.RoomId
        };

        var result = await _repository.AddAsync(session);

        return new SessionDto
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            StartTime = result.StartTime,
            EndTime = result.EndTime,
            EventId = result.EventId,
            RoomId = result.RoomId
        };
    }

    public async Task<SessionDto?> UpdateAsync(SessionDto dto)
    {
        var session = new Session
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            EventId = dto.EventId,
            RoomId = dto.RoomId
        };

        var result = await _repository.UpdateAsync(session);
        if (result == null) return null;

        return new SessionDto
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            StartTime = result.StartTime,
            EndTime = result.EndTime,
            EventId = result.EventId,
            RoomId = result.RoomId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<SessionDto>> GetByEventIdAsync(int eventId)
    {
        var sessions = await _repository.GetByEventIdAsync(eventId);
        return sessions.Select(s => new SessionDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            EventId = s.EventId,
            RoomId = s.RoomId
        });
    }
}
