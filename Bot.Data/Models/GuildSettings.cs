using System.ComponentModel.DataAnnotations;
using Bot.Data.Interfaces;

namespace Bot.Data.Models;

public class GuildSettings : IEntity
{
    [Key]
    public ulong Id { get; set; }
    
    public ulong? KekwChannel { get; set; }
}