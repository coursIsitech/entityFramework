using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class SessionSpeakerService
{
    private readonly ISessionSpeakerRepository _repository;

    public SessionSpeakerService(ISessionSpeakerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SessionSpeakerDto>> GetBySessionIdAsync(int sessionId)
    {
        var list = await _repository.GetBySessionIdAsync(sessionId);
        return list.Select(ss => new SessionSpeakerDto
        {
            SessionId = ss.SessionId,
            SpeakerId = ss.SpeakerId,
            Role = ss.Role
        });
    }

    public async Task<IEnumerable<SessionSpeakerDto>> GetBySpeakerIdAsync(int speakerId)
    {
        var list = await _repository.GetBySpeakerIdAsync(speakerId);
        return list.Select(ss => new SessionSpeakerDto
        {
            SessionId = ss.SessionId,
            SpeakerId = ss.SpeakerId,
            Role = ss.Role
        });
    }

    public async Task<SessionSpeakerDto> AddAsync(SessionSpeakerDto dto)
    {
        var entity = new SessionSpeaker
        {
            SessionId = dto.SessionId,
            SpeakerId = dto.SpeakerId,
            Role = dto.Role
        };

        var result = await _repository.AddAsync(entity);

        return new SessionSpeakerDto
        {
            SessionId = result.SessionId,
            SpeakerId = result.SpeakerId,
            Role = result.Role
        };
    }

    public async Task<bool> DeleteAsync(int sessionId, int speakerId)
    {
        return await _repository.DeleteAsync(sessionId, speakerId);
    }
}