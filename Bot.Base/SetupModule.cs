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
        public Task SetupKekw(string channelMention, int kekwNeededForQuote)
        {
            var quoteChannel = this.Context.Message.MentionedChannels.FirstOrDefault();

            if (quoteChannel is null)
                return ReplyAsync("Channel not found");
            
            guildService.SetupKekwSettings(this.Context.Guild.Id, quoteChannel.Id, kekwNeededForQuote);

            return ReplyAsync($"Channel set: {channelMention}; Kekws needed for quoting: {kekwNeededForQuote}");
        }
    }
}
