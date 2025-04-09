using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location?> GetByIdAsync(int id)
    {
        return await _context.Locations.FindAsync(id);
    }

    public async Task<Location> AddAsync(Location location)
    {
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }

    public async Task<Location?> UpdateAsync(Location location)
    {
        var existing = await _context.Locations.FindAsync(location.Id);
        if (existing == null) return null;

        existing.Name = location.Name;
        existing.Address = location.Address;
        existing.City = location.City;
        existing.Country = location.Country;
        existing.Capacity = location.Capacity;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Locations.FindAsync(id);
        if (existing == null) return false;

        _context.Locations.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}