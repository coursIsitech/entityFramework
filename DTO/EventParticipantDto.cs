namespace MyWebApi.DTO;

public class EventParticipantDto
{
    public int EventId { get; set; }
    public int ParticipantId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AttendanceStatus { get; set; } = null!;
}