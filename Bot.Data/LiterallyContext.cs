using System.Data.Common;
using Bot.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
    
namespace Bot.Data;

public class LiterallyContext : DbContext
{
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<GuildSettings> GuildSettings { get; set; } = null!;
    public DbSet<Quotes> Quotes { get; set; } = null!;
    public DbSet<QuoteAttachment> QuoteAttachments { get; set; } = null!;
    
    public LiterallyContext(DbContextOptions options) : base(options)
    {
        this.Database.Migrate();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // this bs is required because for some reason ef migration generator cannot manage with DbContextOptions
        // :)
        // optionsBuilder.UseNpgsql("Server=XD;Port=XD;Database=XD;Userid=XD;Password=XD");
    }
}
