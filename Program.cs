using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace RedBot
{
    public class Program
    {
        public DiscordSocketClient _client { get; set; }

        static void Main(string[] args) => new Program().MainAsync();
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += OnClientReady;

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
    }
}