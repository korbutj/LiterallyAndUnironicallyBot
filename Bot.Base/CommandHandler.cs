using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private IServiceProvider _serviceProvider;
    
    
    // Retrieve client and CommandService instance via ctor
    public CommandHandler(DiscordSocketClient client, CommandService commands)
    {
        _commands = commands;
        _client = client;
    }
    
    public async Task InstallCommandsAsync(IServiceProvider services)
    {
        // Hook the MessageReceived event into our command handler
        _client.MessageReceived += HandleCommandAsync;
        _client.ReactionAdded += HandleReactionsAsync;
        _serviceProvider = services;
        // Here we discover all of the command modules in the entry 
        // assembly and load them. Starting from Discord.NET 2.0, a
        // service provider is required to be passed into the
        // module registration method to inject the 
        // required dependencies.
        //
        // If you do not use Dependency Injection, pass null.
        // See Dependency Injection guide for more information.
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), 
                                        services: services);
    }

    private async Task HandleReactionsAsync(Cacheable<IUserMessage, ulong> user, Cacheable<IMessageChannel, ulong> message, SocketReaction reaction)
    {
        var reactionsToHandle = reaction.Message.Value.Reactions.Where(x => x.Value.ReactionCount > 5).ToList();

        var tasks = reactionsToHandle
            .Where(x => x.Key.Name.ToLower().Contains("kekw"))
            .Select(x => HandleKekw(x, reaction.Message.Value));

        await Task.WhenAll(tasks);

        if (reactionsToHandle.Any(x => x.Key.Name.ToLower().Contains("cringe")))
        {
            
        }
    }

    private async Task HandleKekw(KeyValuePair<IEmote, ReactionMetadata> dict, SocketMessage message)
    {
        var embedBuilder = new EmbedBuilder();
        
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        // Don't process the command if it was a system message
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        var rng = new Random();
        var randomNumber = rng.NextInt64(100);
        
        if (message.Author.Username == "lukeee6285")
        {
            var text = message.ToString();
            if (text.Length > 20 && text.AsEnumerable().Sum(x => char.IsUpper(x) ? 1 : 0) > (text.Length / 50))
            {
                var emote = ((SocketGuildChannel)message.Channel).Guild.Emotes.First(x => x.Name.ToLower() == "banhammer");
                await message.AddReactionAsync(emote);
                await message.Channel.SendMessageAsync(":banhammer: @lukeee6285");
            }
        }

        if (randomNumber > 97)
        {
            await message.AddReactionAsync(Emoji.Parse(":flag_be:"));
        }
    }
}