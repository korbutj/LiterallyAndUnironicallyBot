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

    public async Task<ulong> CreateQuote(ulong guildId, ulong authorId, string? content, List<string>? attachementUrls = null)
    {
        var quote = new Quotes()
        {
            AuthorId = authorId,
            Content = content,
            GuildId = guildId
        };

        var result = await repository.Upsert(quote);

        if (attachementUrls != null && attachementUrls.Any())
        {
            var attachements = attachementUrls
                .Select(x => new QuoteAttachment()
                    { QuoteId = result.Id, AttachmentUrl = x }).ToList();

            await repository.Upsert(attachements);
        }

        return quote.Id;
    }
}