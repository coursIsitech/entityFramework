using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class LocationService
{
    private readonly ILocationRepository _repository;

    public LocationService(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LocationDto>> GetAllAsync()
    {
        var locations = await _repository.GetAllAsync();
        return locations.Select(l => new LocationDto
        {
            Id = l.Id,
            Name = l.Name,
            Address = l.Address,
            City = l.City,
            Country = l.Country,
            Capacity = l.Capacity
        });
    }

    public async Task<LocationDto?> GetByIdAsync(int id)
    {
        var location = await _repository.GetByIdAsync(id);
        if (location == null) return null;

        return new LocationDto
        {
            Id = location.Id,
            Name = location.Name,
            Address = location.Address,
            City = location.City,
            Country = location.Country,
            Capacity = location.Capacity
        };
    }

    public async Task<LocationDto> AddAsync(LocationDto dto)
    {
        var location = new Location
        {
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country,
            Capacity = dto.Capacity
        };

        var result = await _repository.AddAsync(location);

        return new LocationDto
        {
            Id = result.Id,
            Name = result.Name,
            Address = result.Address,
            City = result.City,
            Country = result.Country,
            Capacity = result.Capacity
        };
    }

    public async Task<LocationDto?> UpdateAsync(LocationDto dto)
    {
        var location = new Location
        {
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country,
            Capacity = dto.Capacity
        };

        var result = await _repository.UpdateAsync(location);
        if (result == null) return null;

        return new LocationDto
        {
            Id = result.Id,
            Name = result.Name,
            Address = result.Address,
            City = result.City,
            Country = result.Country,
            Capacity = result.Capacity
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
