using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;

public class Program
{
    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.SlashCommandExecuted += _client_SlashCommandExecuted;
        _client.Log += Log;
        _client.Ready += Client_Ready;
        var token = File.ReadAllText("C:\\token.txt");
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        await Task.Delay(-1);
    }

    private async Task _client_SlashCommandExecuted(SocketSlashCommand command)
    {

        await command.RespondAsync($"You executed {command.Data.Name}");


   

    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public async Task Client_Ready()
    {
      await  _client.SetGameAsync("Resor", null, ActivityType.Watching);

        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("resa");
        globalCommand.WithDescription("Hittar en bra resa");
        globalCommand.AddOption("antalpersoner", ApplicationCommandOptionType.Integer, "Antal personer som ska åka", true);

        try
        {
            // With global commands we don't need the guild.
            await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
            // Using the ready event is a simple implementation for the sake of the example. Suitable for testing and development.
            // For a production bot, it is recommended to only run the CreateGlobalApplicationCommandAsync() once for each command.
        }
        catch (ApplicationCommandException exception)
        {
            // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

            // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
            Console.WriteLine(json);
        }
    }
}