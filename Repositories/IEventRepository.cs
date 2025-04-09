using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<Event> AddAsync(Event ev);
    Task<Event?> UpdateAsync(Event ev);
    Task<bool> DeleteAsync(int id);
}