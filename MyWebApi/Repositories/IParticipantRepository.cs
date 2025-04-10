using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface IParticipantRepository
{
    Task<IEnumerable<Participant>> GetAllAsync();
    Task<Participant?> GetByIdAsync(int id);
    Task<Participant> AddAsync(Participant participant);
    Task<Participant?> UpdateAsync(Participant participant);
    Task<bool> DeleteAsync(int id);
}