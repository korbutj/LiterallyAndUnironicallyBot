using System.Reflection;
using Bot.Data.Models;
using Bot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private IServiceProvider _serviceProvider;
    private readonly IGuildService guildService;


    // Retrieve client and CommandService instance via ctor
    public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider serviceProvider, IGuildService guildService)
    {
        _commands = commands;
        _serviceProvider = serviceProvider;
        this.guildService = guildService;
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

    private async Task HandleReactionsAsync(Cacheable<IUserMessage, ulong> message, Cacheable<IMessageChannel, ulong> channel, SocketReaction reaction)
    {
        var guildChannel = (channel.Value as SocketGuildChannel);
        var userMessage = await guildChannel.Guild.GetTextChannel(channel.Id).GetMessageAsync(message.Id);
        
        var guild = guildChannel?.Guild;
        if (guild is null)
            return;
        
        var settings = await guildService.GetSettings(guild.Id);

        if (settings?.KekwReactionsNeeded is null || settings?.KekwChannel is null)
            return;

        var handleMessage = userMessage.Reactions
            .Where(x => x.Key.Name.ToLower().Contains("kekw"))
            .Any(x => x.Value.ReactionCount >= settings.KekwReactionsNeeded);

        if (handleMessage)
        {
            await SaveQoute(userMessage, guild);
            
            await SendQuote(userMessage, guild, settings);
        }
    }

    private async Task SaveQoute(IMessage userMessage, SocketGuild guild)
    {
        var attachmentUrls = userMessage.Attachments.Select(x => x.Url).ToList();

        await guildService.CreateQuote(guild.Id, userMessage.Author.Id, userMessage.Content, attachmentUrls);
    }

    private async Task SendQuote(IMessage message, SocketGuild guildSocket, GuildSettings guildSettings)
    {
        var embedBuilder = new EmbedBuilder
        {
            Author = new EmbedAuthorBuilder() 
            { 
                Name = message.Author.Username,
                IconUrl = message.Author.GetAvatarUrl() 
            }
        };

        embedBuilder.WithDescription(message.Content);
        if (message.Attachments.Any())
            embedBuilder.WithImageUrl(message.Attachments.First().Url);

        var embed = embedBuilder.Build();
        
        await guildSocket.GetTextChannel(guildSettings.KekwChannel.Value)?.SendMessageAsync("", false, embed);
    }
    
    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        // Don't process the command if it was a system message
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        // Create a number to track where the prefix ends and the command begins
        int argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        if (!(message.HasCharPrefix('!', ref argPos) || 
              message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await _commands.ExecuteAsync(
            context: context, 
            argPos: argPos,
            services: _serviceProvider);
    }
}