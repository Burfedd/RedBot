using Discord;
using Discord.WebSocket;

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
    }
}