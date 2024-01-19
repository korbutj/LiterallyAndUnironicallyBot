using Bot.Data;
using Bot.Services;
using Discord.Commands;

namespace Bot.Base.Modules
{
    public class SetupModule : ModuleBase<SocketCommandContext>
    {
        private readonly IGuildService guildService;

        public SetupModule(IGuildService guildService)
        {
            this.guildService = guildService;
        }

        [Command("SetupKekw")]
        public async Task SetupKekw(string channelMention, int kekwNeededForQuote)
        {
            var quoteChannel = this.Context.Message.MentionedChannels.FirstOrDefault();

            if (quoteChannel is null)
            {
                await ReplyAsync("Channel not found");
                return;
            }
            
            await guildService.SetupKekwSettings(this.Context.Guild.Id, quoteChannel.Id, kekwNeededForQuote);

            await ReplyAsync($"Channel set: {channelMention}; Kekws needed for quoting: {kekwNeededForQuote}");
        }
    }
}
