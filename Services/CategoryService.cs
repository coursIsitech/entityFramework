using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> AddAsync(CategoryDto dto)
    {
        var category = new Category { Name = dto.Name };
        var result = await _repository.AddAsync(category);
        return new CategoryDto { Id = result.Id, Name = result.Name };
    }

    public async Task<CategoryDto?> UpdateAsync(CategoryDto dto)
    {
        var category = new Category { Id = dto.Id, Name = dto.Name };
        var result = await _repository.UpdateAsync(category);
        return result == null ? null : new CategoryDto { Id = result.Id, Name = result.Name };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}