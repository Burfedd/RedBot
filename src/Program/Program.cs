using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedBot.Feature.ShellExec.Services;
using RedBot.Feature.WeatherForecast.Services;
using RedBot.Startup;

namespace RedBot
{
    public class Program
    {
        private readonly IServiceProvider _serviceProvider;
        private DiscordSocketClient? _client;

        public Program()
        {
            _serviceProvider = CreateProvider();
        }

        static IServiceProvider CreateProvider()
        {
            ServiceCollection collection = new ServiceCollection();
            DependencyInjection.AddServices(collection);
            return collection.BuildServiceProvider();
        }

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += OnClientReady;
            _client.SlashCommandExecuted += OnSlashCommandExecuted;

            string token = Constants.Bot.Token;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }

        private async Task OnClientReady()
        {
            SocketGuild guild = _client.GetGuild(Constants.Guilds.TestGuild.Id);
            SlashCommandBuilder guildCommand = new SlashCommandBuilder()
                .WithName("ping")
                .WithDescription("Ping Pong!");

            try
            {
                await guild.CreateApplicationCommandAsync(guildCommand.Build());
            }
            catch (HttpException ex)
            {
                string json = JsonConvert.SerializeObject(ex);
                await Console.Out.WriteLineAsync(json);
            }
        }

        private async Task OnSlashCommandExecuted(SocketSlashCommand command)
        {
            switch (command.CommandName)
            {
                case "weather":
                    {
                        IWeatherForecastService? weatherService = _serviceProvider.GetService<IWeatherForecastService>();
                        if (weatherService != null)
                        {
                            await command.RespondAsync(weatherService.GetForecast(string.Empty));
                        }
                        break;
                    }
                case "shell":
                    {
                        IShellCommandService? shellService = _serviceProvider.GetService<IShellCommandService>();
                        if (shellService != null)
                        {
                            await command.RespondAsync(shellService.ExecuteCommand(string.Empty));
                        }
                        break;
                    }
                default: break;
            }
        }
    }
}