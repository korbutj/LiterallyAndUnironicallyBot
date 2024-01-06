using Bot.Data;

namespace Bot.Services;

public class GuildService : IGuildService
{
    private readonly IRepository<LiterallyContext> repository;

    public GuildService(IRepository<LiterallyContext> repository)
    {
        this.repository = repository;
    }

    public Task SetKekwChannel(ulong guildId, ulong channelId)
    {
        throw new NotImplementedException();
    }
}