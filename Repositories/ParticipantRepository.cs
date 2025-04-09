using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public ParticipantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Participant>> GetAllAsync()
    {
        return await _context.Participants.ToListAsync();
    }

    public async Task<Participant?> GetByIdAsync(int id)
    {
        return await _context.Participants.FindAsync(id);
    }

    public async Task<Participant> AddAsync(Participant participant)
    {
        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();
        return participant;
    }

    public async Task<Participant?> UpdateAsync(Participant participant)
    {
        var existing = await _context.Participants.FindAsync(participant.Id);
        if (existing == null)
            return null;

        existing.FirstName = participant.FirstName;
        existing.LastName = participant.LastName;
        existing.Email = participant.Email;
        existing.Company = participant.Company;
        existing.JobTitle = participant.JobTitle;

        await _context.SaveChangesAsync();

        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Participants.FindAsync(id);
        if (existing == null)
            return false;

        _context.Participants.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}