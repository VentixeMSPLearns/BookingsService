namespace Business.Models;

public class BookingModel
{
    public string Id { get; set; } = null!; //PK
    public string UserId { get; set; } = null!; //FK
    public string EventId { get; set; } = null!; //FK

}
