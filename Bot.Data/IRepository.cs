using Bot.Data.Interfaces;
using Bot.Data.Models;

namespace Bot.Data;

public interface IRepository<DbContext>
{
    Task<T> Upsert<T>(T entity) where T : class, IEntity;
    Task<List<T>> Upsert<T>(List<T> entity) where T : class, IEntity;
    Task<T?> Get<T>(ulong id) where T : class, IEntity;
}