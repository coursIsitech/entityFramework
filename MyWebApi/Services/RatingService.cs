using MyWebApi.DTO;
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services;

public class RatingService
{
    private readonly IRatingRepository _repository;

    public RatingService(IRatingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RatingDto>> GetBySessionIdAsync(int sessionId)
    {
        var ratings = await _repository.GetBySessionIdAsync(sessionId);
        return ratings.Select(r => new RatingDto
        {
            Id = r.Id,
            SessionId = r.SessionId,
            ParticipantId = r.ParticipantId,
            Score = r.Score,
            Comment = r.Comment
        });
    }

    public async Task<IEnumerable<RatingDto>> GetByParticipantIdAsync(int participantId)
    {
        var ratings = await _repository.GetByParticipantIdAsync(participantId);
        return ratings.Select(r => new RatingDto
        {
            Id = r.Id,
            SessionId = r.SessionId,
            ParticipantId = r.ParticipantId,
            Score = r.Score,
            Comment = r.Comment
        });
    }

    public async Task<RatingDto> AddAsync(RatingDto dto)
    {
        var rating = new Rating
        {
            SessionId = dto.SessionId,
            ParticipantId = dto.ParticipantId,
            Score = dto.Score,
            Comment = dto.Comment
        };

        var result = await _repository.AddAsync(rating);

        return new RatingDto
        {
            Id = result.Id,
            SessionId = result.SessionId,
            ParticipantId = result.ParticipantId,
            Score = result.Score,
            Comment = result.Comment
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}