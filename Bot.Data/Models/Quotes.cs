using System.ComponentModel.DataAnnotations;
using Bot.Data.Interfaces;

namespace Bot.Data.Models;

public class Quotes : IEntity
{
    [Key]
    public ulong Id { get; set; }

    public string? Content { get; set; }
    public ulong AuthorId { get; set; }

    public ulong GuildId { get; set; }
    public virtual GuildSettings? Guild { get; set; }
}