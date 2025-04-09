using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class ParticipantService
{
    private readonly IParticipantRepository _repository;

    public ParticipantService(IParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ParticipantDto>> GetAllAsync()
    {
        var participants = await _repository.GetAllAsync();
        return participants.Select(p => new ParticipantDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email,
            Company = p.Company,
            JobTitle = p.JobTitle
        });
    }

    public async Task<ParticipantDto?> GetByIdAsync(int id)
    {
        var participant = await _repository.GetByIdAsync(id);
        if (participant == null) return null;

        return new ParticipantDto
        {
            Id = participant.Id,
            FirstName = participant.FirstName,
            LastName = participant.LastName,
            Email = participant.Email,
            Company = participant.Company,
            JobTitle = participant.JobTitle
        };
    }

    public async Task<ParticipantDto> AddAsync(ParticipantDto dto)
    {
        var participant = new Participant
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Company = dto.Company,
            JobTitle = dto.JobTitle
        };

        var result = await _repository.AddAsync(participant);

        return new ParticipantDto
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Company = result.Company,
            JobTitle = result.JobTitle
        };
    }

    public async Task<ParticipantDto?> UpdateAsync(ParticipantDto dto)
    {
        var participant = new Participant
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Company = dto.Company,
            JobTitle = dto.JobTitle
        };

        var result = await _repository.UpdateAsync(participant);
        if (result == null) return null;

        return new ParticipantDto
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Company = result.Company,
            JobTitle = result.JobTitle
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
