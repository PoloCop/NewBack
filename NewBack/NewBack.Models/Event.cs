using System.ComponentModel.DataAnnotations;

namespace NewBack.Models;

public class Event
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
}