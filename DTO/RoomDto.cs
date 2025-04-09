namespace MyWebApi.DTO;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public int LocationId { get; set; }
}