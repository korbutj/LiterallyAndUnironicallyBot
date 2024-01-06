using Bot.Data;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

namespace Bot.Base
{
    class Program
    {
        private DiscordSocketClient client = null!;
        private IConfiguration config = null!;
        
        private readonly DiscordSocketConfig socketConfig = new()
        {
            GatewayIntents = GatewayIntents.All,
            AlwaysDownloadUsers = true,
        };
        
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient(socketConfig);
            config = BuildConfig();
            
            var services = ConfigureServices();

            await services.GetRequiredService<CommandHandler>().InstallCommandsAsync(services);

            await client.LoginAsync(TokenType.Bot, config["token"]);
            await client.StartAsync();
        }
        
        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection()
                // Base
                .AddSingleton(client)
                .AddSingleton<CommandService>()
                // .AddSingleton<CommandHandlingService>()
                .AddSingleton<CommandHandler>()
                // Logging
                .AddLogging()
                // Extra
                .AddSingleton(config)
                .AddDbContext<LiterallyContext>();
            
            return services.BuildServiceProvider();
        }

        private IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}