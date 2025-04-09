using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class RoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        var rooms = await _repository.GetAllAsync();
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            Name = r.Name,
            Capacity = r.Capacity,
            LocationId = r.LocationId
        });
    }

    public async Task<RoomDto?> GetByIdAsync(int id)
    {
        var room = await _repository.GetByIdAsync(id);
        if (room == null) return null;

        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            LocationId = room.LocationId
        };
    }

    public async Task<RoomDto> AddAsync(RoomDto dto)
    {
        var room = new Room
        {
            Name = dto.Name,
            Capacity = dto.Capacity,
            LocationId = dto.LocationId
        };

        var result = await _repository.AddAsync(room);

        return new RoomDto
        {
            Id = result.Id,
            Name = result.Name,
            Capacity = result.Capacity,
            LocationId = result.LocationId
        };
    }

    public async Task<RoomDto?> UpdateAsync(RoomDto dto)
    {
        var room = new Room
        {
            Id = dto.Id,
            Name = dto.Name,
            Capacity = dto.Capacity,
            LocationId = dto.LocationId
        };

        var result = await _repository.UpdateAsync(room);
        if (result == null) return null;

        return new RoomDto
        {
            Id = result.Id,
            Name = result.Name,
            Capacity = result.Capacity,
            LocationId = result.LocationId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<RoomDto>> GetByLocationIdAsync(int locationId)
    {
        var rooms = await _repository.GetByLocationIdAsync(locationId);
        return rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            Name = r.Name,
            Capacity = r.Capacity,
            LocationId = r.LocationId
        });
    }
}
