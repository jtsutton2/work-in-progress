using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

class Program
{
    private DiscordSocketClient _client;
    private const string Token = "YOUR_DISCORD_BOT_TOKEN";

    private static readonly List<string> Quests = new()
    {
        "Retrieve the lost artifact from the dark forest.",
        "Defeat the rogue knight terrorizing the village.",
        "Escort a merchant safely through goblin-infested roads.",
        "Solve the ancient puzzle hidden in the ruins.",
        "Gather rare herbs for the town's healer."
    };

    static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

    public async Task RunBotAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.MessageReceived += HandleCommandAsync;
        
        await _client.LoginAsync(TokenType.Bot, Token);
        await _client.StartAsync();

        await Task.Delay(-1); // Keep the bot running
    }

    private async Task HandleCommandAsync(SocketMessage message)
    {
        if (message.Author.IsBot || message.Content.ToLower() != "!quest") return;

        Random rand = new();
        string quest = Quests[rand.Next(Quests.Count)];

        await message.Channel.SendMessageAsync($"🛡️ Your quest: {quest}");
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }
}
