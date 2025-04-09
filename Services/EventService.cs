using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class EventService
{
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await _repository.GetAllAsync();
        return events.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Status = e.Status,
            CategoryId = e.CategoryId,
            LocationId = e.LocationId
        });
    }

    public async Task<EventDto?> GetByIdAsync(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Status = e.Status,
            CategoryId = e.CategoryId,
            LocationId = e.LocationId
        };
    }

    public async Task<EventDto> AddAsync(EventDto dto)
    {
        var e = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            CategoryId = dto.CategoryId,
            LocationId = dto.LocationId
        };

        var result = await _repository.AddAsync(e);

        return new EventDto
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Status = result.Status,
            CategoryId = result.CategoryId,
            LocationId = result.LocationId
        };
    }

    public async Task<EventDto?> UpdateAsync(EventDto dto)
    {
        var e = new Event
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            CategoryId = dto.CategoryId,
            LocationId = dto.LocationId
        };

        var result = await _repository.UpdateAsync(e);
        if (result == null) return null;

        return new EventDto
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Status = result.Status,
            CategoryId = result.CategoryId,
            LocationId = result.LocationId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
