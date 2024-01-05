using Bot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Location> Locations { get; set; }

    public DbContext(DbContextOptionsBuilder options)
    {
        options.UseSqlite(@"DataSource=literallyDatabase.db");
    }
}