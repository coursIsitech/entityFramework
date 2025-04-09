using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface ISessionSpeakerRepository
{
    Task<IEnumerable<SessionSpeaker>> GetBySessionIdAsync(int sessionId);
    Task<IEnumerable<SessionSpeaker>> GetBySpeakerIdAsync(int speakerId);
    Task<SessionSpeaker> AddAsync(SessionSpeaker sessionSpeaker);
    Task<bool> DeleteAsync(int sessionId, int speakerId);
}