using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bot.Data.Interfaces;

namespace Bot.Data.Models;

public class GuildSettings : IEntity
{
    [Key] 
    public Guid Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public ulong GuildId { get; set; }

    public string QuoteEmote { get; set; } = "kekw";
    public ulong? KekwChannel { get; set; }
    public int? KekwReactionsNeeded { get; set; }
}