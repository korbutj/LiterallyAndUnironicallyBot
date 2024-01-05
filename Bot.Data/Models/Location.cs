
using System.ComponentModel.DataAnnotations;

namespace Bot.Data.Models;

public class Location
{
    [Key]
    public string? Name { get; set; }
    
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}