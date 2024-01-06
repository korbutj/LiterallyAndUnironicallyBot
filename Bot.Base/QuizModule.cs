using Bot.Data;
using Bot.Services;
using Discord.Commands;

namespace Bot.Base.Modules
{
    public class QuizModule : ModuleBase<SocketCommandContext>
    {
        private readonly IGuildService guildService;

        public QuizModule(IGuildService guildService)
        {
            this.guildService = guildService;
        }

        [Command("SetKekwChannel")]
        public Task SetQuoteChannel(string channelMention)
        {

            var quoteChannel = this.Context.Message.MentionedChannels.FirstOrDefault();
            

            guildService.SetKekwChannel(this.Context.Guild.Id, quoteChannel.Id);

            return ReplyAsync($"Channel set: {channelMention}");
        }
        
    }
}
