using Bot.Data;
using Bot.Data.Models;

namespace Bot.Services;

public class GuildService : IGuildService
{
    private readonly IRepository<LiterallyContext> repository;

    public GuildService(IRepository<LiterallyContext> repository)
    {
        this.repository = repository;
    }

    public async Task SetupKekwSettings(ulong guildId, ulong channelId, int kekwNeeded)
    {
        var guild = await repository.Get<GuildSettings>(guildId) ?? new GuildSettings() { Id = guildId };
        guild.KekwChannel = channelId;
        guild.KekwReactionsNeeded = kekwNeeded;

        await repository.Upsert(guild);
    }

    public async Task<GuildSettings?> GetSettings(ulong guildId)
    {
        return await repository.Get<GuildSettings>(guildId);
    }
}