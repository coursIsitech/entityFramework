using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface IEventParticipantRepository
{
    Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId);
    Task<IEnumerable<EventParticipant>> GetByParticipantIdAsync(int participantId);
    Task<EventParticipant> AddAsync(EventParticipant eventParticipant);
    Task<bool> DeleteAsync(int eventId, int participantId);
}