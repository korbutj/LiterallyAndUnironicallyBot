using Bot.Data.Interfaces;
using Bot.Data.Models;

namespace Bot.Data;

public interface IRepository<DbContext>
{
    Task Upsert<T>(T settings) where T : class, IEntity;
    Task<T?> Get<T>(ulong id) where T : class, IEntity;
}