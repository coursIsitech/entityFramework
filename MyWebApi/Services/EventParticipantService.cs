using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class EventParticipantService
{
    private readonly IEventParticipantRepository _repository;

    public EventParticipantService(IEventParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EventParticipantDto>> GetByEventIdAsync(int eventId)
    {
        var list = await _repository.GetByEventIdAsync(eventId);
        return list.Select(ep => new EventParticipantDto
        {
            EventId = ep.EventId,
            ParticipantId = ep.ParticipantId,
            RegistrationDate = ep.RegistrationDate,
            AttendanceStatus = ep.AttendanceStatus
        });
    }

    public async Task<IEnumerable<EventParticipantDto>> GetByParticipantIdAsync(int participantId)
    {
        var list = await _repository.GetByParticipantIdAsync(participantId);
        return list.Select(ep => new EventParticipantDto
        {
            EventId = ep.EventId,
            ParticipantId = ep.ParticipantId,
            RegistrationDate = ep.RegistrationDate,
            AttendanceStatus = ep.AttendanceStatus
        });
    }

    public async Task<EventParticipantDto> AddAsync(EventParticipantDto dto)
    {
        var entity = new EventParticipant
        {
            EventId = dto.EventId,
            ParticipantId = dto.ParticipantId,
            RegistrationDate = dto.RegistrationDate,
            AttendanceStatus = dto.AttendanceStatus
        };

        var result = await _repository.AddAsync(entity);

        return new EventParticipantDto
        {
            EventId = result.EventId,
            ParticipantId = result.ParticipantId,
            RegistrationDate = result.RegistrationDate,
            AttendanceStatus = result.AttendanceStatus
        };
    }

    public async Task<bool> DeleteAsync(int eventId, int participantId)
    {
        return await _repository.DeleteAsync(eventId, participantId);
    }
}
