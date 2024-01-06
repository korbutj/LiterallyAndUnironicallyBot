using System.ComponentModel.DataAnnotations;
using Bot.Data.Interfaces;

namespace Bot.Data.Models;

public class GuildSettings : IEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid? KekwChannel { get; set; }
}