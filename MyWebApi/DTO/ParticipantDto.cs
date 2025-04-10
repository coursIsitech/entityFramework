namespace MyWebApi.DTO;

public class ParticipantDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Company { get; set; }
    public string? JobTitle { get; set; }
}