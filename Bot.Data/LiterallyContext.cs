using Bot.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
    
namespace Bot.Data;

public class LiterallyContext : DbContext
{
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<GuildSettings> GuildSettings { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}