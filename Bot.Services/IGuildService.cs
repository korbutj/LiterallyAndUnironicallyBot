using Bot.Data.Models;

namespace Bot.Services;

public interface IGuildService
{
    Task SetupKekwSettings(ulong guildId, ulong channelId, int kekwNeeded);
    Task<GuildSettings?> GetSettings(ulong guildId);
}