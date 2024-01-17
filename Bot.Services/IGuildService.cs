using Bot.Data.Models;

namespace Bot.Services;

public interface IGuildService
{
    Task SetupKekwSettings(ulong guildId, ulong channelId, int kekwNeeded);
    Task<GuildSettings?> GetSettings(ulong guildId);

    Task<ulong> CreateQuote(ulong guildId, ulong authorId, string? content, List<string>? attachementUrls = null);
}