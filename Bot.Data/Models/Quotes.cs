using System.ComponentModel.DataAnnotations;

namespace Bot.Data.Models;

public class Quotes
{
    [Key]
    public ulong Id { get; set; }

    public string? Content { get; set; }
    public ulong AuthorId { get; set; }

    public ulong GuildId { get; set; }
    public virtual GuildSettings? Guild { get; set; }
}