using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly ApplicationDbContext _context;

    public RatingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rating>> GetBySessionIdAsync(int sessionId)
    {
        return await _context.Ratings
            .Where(r => r.SessionId == sessionId)
            .Include(r => r.Session)
            .Include(r => r.Participant)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rating>> GetByParticipantIdAsync(int participantId)
    {
        return await _context.Ratings
            .Where(r => r.ParticipantId == participantId)
            .Include(r => r.Session)
            .ToListAsync();
    }

    public async Task<Rating> AddAsync(Rating rating)
    {
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
        return rating;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Ratings.FindAsync(id);
        if (existing == null) return false;

        _context.Ratings.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}