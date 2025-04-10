using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.Location)
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.Location)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event> AddAsync(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
        return ev;
    }

    public async Task<Event?> UpdateAsync(Event ev)
    {
        var existing = await _context.Events.FindAsync(ev.Id);
        if (existing == null)
            return null;

        existing.Title = ev.Title;
        existing.Description = ev.Description;
        existing.StartDate = ev.StartDate;
        existing.EndDate = ev.EndDate;
        existing.Status = ev.Status;
        existing.CategoryId = ev.CategoryId;
        existing.LocationId = ev.LocationId;

        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Events.FindAsync(id);
        if (existing == null)
            return false;

        _context.Events.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}