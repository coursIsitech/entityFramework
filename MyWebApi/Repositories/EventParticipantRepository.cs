using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class EventParticipantRepository : IEventParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public EventParticipantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId)
    {
        return await _context.EventParticipants
            .Where(ep => ep.EventId == eventId)
            .Include(ep => ep.Participant)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventParticipant>> GetByParticipantIdAsync(int participantId)
    {
        return await _context.EventParticipants
            .Where(ep => ep.ParticipantId == participantId)
            .Include(ep => ep.Event)
            .ToListAsync();
    }

    public async Task<EventParticipant> AddAsync(EventParticipant eventParticipant)
    {
        _context.EventParticipants.Add(eventParticipant);
        await _context.SaveChangesAsync();
        return eventParticipant;
    }

    public async Task<bool> DeleteAsync(int eventId, int participantId)
    {
        var existing = await _context.EventParticipants
            .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);

        if (existing == null)
            return false;

        _context.EventParticipants.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}