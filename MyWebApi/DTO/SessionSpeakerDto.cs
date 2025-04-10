namespace MyWebApi.DTO;

public class SessionSpeakerDto
{
    public int SessionId { get; set; }
    public int SpeakerId { get; set; }
    public string Role { get; set; } = null!;
}