using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class SessionSpeakerRepository : ISessionSpeakerRepository
{
    private readonly ApplicationDbContext _context;

    public SessionSpeakerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SessionSpeaker>> GetBySessionIdAsync(int sessionId)
    {
        return await _context.SessionSpeakers
            .Where(ss => ss.SessionId == sessionId)
            .Include(ss => ss.Speaker)
            .ToListAsync();
    }

    public async Task<IEnumerable<SessionSpeaker>> GetBySpeakerIdAsync(int speakerId)
    {
        return await _context.SessionSpeakers
            .Where(ss => ss.SpeakerId == speakerId)
            .Include(ss => ss.Session)
            .ToListAsync();
    }

    public async Task<SessionSpeaker> AddAsync(SessionSpeaker sessionSpeaker)
    {
        _context.SessionSpeakers.Add(sessionSpeaker);
        await _context.SaveChangesAsync();
        return sessionSpeaker;
    }

    public async Task<bool> DeleteAsync(int sessionId, int speakerId)
    {
        var existing = await _context.SessionSpeakers
            .FirstOrDefaultAsync(ss => ss.SessionId == sessionId && ss.SpeakerId == speakerId);

        if (existing == null)
            return false;

        _context.SessionSpeakers.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}