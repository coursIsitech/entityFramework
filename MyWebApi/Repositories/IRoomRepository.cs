using MyWebApi.Models;

namespace MyWebApi.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<Room> AddAsync(Room room);
    Task<Room?> UpdateAsync(Room room);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Room>> GetByLocationIdAsync(int locationId);
}