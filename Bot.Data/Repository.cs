using Bot.Data.Interfaces;
using Bot.Data.Models;

namespace Bot.Data;

public class Repository : IRepository<LiterallyContext>
{
    private readonly LiterallyContext context;

    public Repository(LiterallyContext context)
    {
        this.context = context;
    }

    public async Task Upsert<T>(T obj) where T : class, IEntity
    {
        var result = context.Set<T>().FirstOrDefault(x => x.Id == obj.Id);
        
        if (result == null)
            await context.Set<T>().AddAsync(obj);
        else
            context.Entry(result).CurrentValues.SetValues(obj);

        await context.SaveChangesAsync();
    }
}