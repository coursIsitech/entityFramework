using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Session>> GetAllAsync()
    {
        return await _context.Sessions
            .Include(s => s.Event)
            .Include(s => s.Room)
            .ToListAsync();
    }

    public async Task<Session?> GetByIdAsync(int id)
    {
        return await _context.Sessions
            .Include(s => s.Event)
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Session> AddAsync(Session session)
    {
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<Session?> UpdateAsync(Session session)
    {
        var existing = await _context.Sessions.FindAsync(session.Id);
        if (existing == null)
            return null;

        existing.Title = session.Title;
        existing.Description = session.Description;
        existing.StartTime = session.StartTime;
        existing.EndTime = session.EndTime;
        existing.EventId = session.EventId;
        existing.RoomId = session.RoomId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Sessions.FindAsync(id);
        if (existing == null)
            return false;

        _context.Sessions.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Session>> GetByEventIdAsync(int eventId)
    {
        return await _context.Sessions
            .Where(s => s.EventId == eventId)
            .Include(s => s.Room)
            .ToListAsync();
    }
}