namespace MyWebApi.Models;

public class Speaker
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Bio { get; set; }
    public string Email { get; set; } = null!;
    public string? Company { get; set; }
    public ICollection<SessionSpeaker> Sessions { get; set; } = new List<SessionSpeaker>();
}