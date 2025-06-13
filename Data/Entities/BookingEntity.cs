using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class BookingEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = null!; //FK
    public string EventId { get; set; } = null!; //FK
}
