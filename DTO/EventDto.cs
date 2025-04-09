namespace MyWebApi.DTO;

public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = null!;
    public int CategoryId { get; set; }
    public int LocationId { get; set; }
}