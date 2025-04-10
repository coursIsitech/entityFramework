using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> GetAllAsync();
    Task<Session?> GetByIdAsync(int id);
    Task<Session> AddAsync(Session session);
    Task<Session?> UpdateAsync(Session session);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Session>> GetByEventIdAsync(int eventId);
}