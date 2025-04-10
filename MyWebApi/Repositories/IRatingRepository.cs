using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface IRatingRepository
{
    Task<IEnumerable<Rating>> GetBySessionIdAsync(int sessionId);
    Task<IEnumerable<Rating>> GetByParticipantIdAsync(int participantId);
    Task<Rating> AddAsync(Rating rating);
    Task<bool> DeleteAsync(int id);
}