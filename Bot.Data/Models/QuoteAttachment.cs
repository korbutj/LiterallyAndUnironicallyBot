using System.ComponentModel.DataAnnotations;
using Bot.Data.Interfaces;

namespace Bot.Data.Models;

public class QuoteAttachment : IEntity
{
    [Key]
    public ulong Id { get; set; }
    public ulong QuoteId { get; set; }
    public string? AttachmentUrl { get; set; } = null!;
    
    public virtual Quotes Quote { get; set; } = null!;

}