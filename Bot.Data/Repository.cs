using System.Linq.Expressions;
using Bot.Data.Interfaces;
using Bot.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Bot.Data;

public class Repository : IRepository<LiterallyContext>
{
    private readonly LiterallyContext context;

    public Repository(LiterallyContext context)
    {
        this.context = context;
    }

    public async Task<T> Upsert<T>(T entity) where T : class, IEntity
    {
        var result = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
        
        if (result == null)
            await context.Set<T>().AddAsync(entity);
        else
            context.Entry(result).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync();

        return entity;
    }
    
    public async Task<List<T>> Upsert<T>(List<T> entities) where T : class, IEntity
    {
        foreach(var entity in entities)
        {
            var result = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (result == null)
                await context.Set<T>().AddAsync(entity);
            else
                context.Entry(result).CurrentValues.SetValues(entity);
        }
        
        await context.SaveChangesAsync();

        return entities;
    }

    public async Task<T?> Get<T>(Guid id) where T : class, IEntity
    {
        var result = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }

    public async Task<T?> Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity
    {
        var result = await context.Set<T>().FirstOrDefaultAsync(predicate);

        return result;
    }
}