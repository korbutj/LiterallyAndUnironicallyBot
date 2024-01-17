using System.ComponentModel.DataAnnotations;
using Bot.Data.Interfaces;

namespace Bot.Data.Models;

public class GuildSettings : IEntity
{
    [Key]
    public ulong Id { get; set; }

    public string QuoteEmote { get; set; } = "kekw";
    public ulong? KekwChannel { get; set; }
    public int? KekwReactionsNeeded { get; set; }
}