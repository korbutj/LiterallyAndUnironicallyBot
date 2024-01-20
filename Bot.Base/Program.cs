using Bot.Data;
using Bot.Services;
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
            AlwaysDownloadUsers = true
        };
        
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            client = new DiscordSocketClient(socketConfig);
            config = BuildConfig();
            
            var services = ConfigureServices();

            await services.GetRequiredService<CommandHandler>().InstallCommandsAsync(services);

            await client.LoginAsync(TokenType.Bot, config["token"]);
            await client.StartAsync();
            
            // COMMENT THIS IF YOU WANT TO RUN
            // DOTNET EF MIGRATIONS ADD
            await Task.Delay(-1);
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
                .AddDbContext<LiterallyContext>(opt => opt.UseNpgsql(config["literallyConnectionString"])) //
                .AddScoped<IRepository<LiterallyContext>, Repository>()
                .AddScoped<IGuildService, GuildService>();
            
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