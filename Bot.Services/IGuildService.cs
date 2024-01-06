namespace Bot.Services;

public interface IGuildService
{
    Task SetKekwChannel(ulong guildId, ulong channelId);
}