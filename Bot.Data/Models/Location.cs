
using System.ComponentModel.DataAnnotations;

namespace Bot.Data.Models;

public class Location
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}