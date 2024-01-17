using System.ComponentModel.DataAnnotations;

namespace Bot.Data.Models;

public class QuoteAttachment
{
    [Key]
    public ulong Id { get; set; }

    public string? AttachmentUrl { get; set; } = null!;
}