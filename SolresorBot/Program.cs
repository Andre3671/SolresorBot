using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

public class Program
{
    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.SlashCommandExecuted += _client_SlashCommandExecuted;
        _client.Log += Log;
        //_client.Ready += Client_Ready;
       

        //  You can assign your bot token to a string, and pass that in to connect.
        //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
        

        // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
        // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
        var token = File.ReadAllText("C:\\token.txt");
        // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        
        // Block this task until the program is closed.
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

    //public async Task Client_Ready()
    //{
    //    // Let's build a guild command! We're going to need a guild so lets just put that in a variable.
    //    var guild = _client.GetGuild(guildId);

    //    // Next, lets create our slash command builder. This is like the embed builder but for slash commands.
    //    var guildCommand = new SlashCommandBuilder();

    //    // Note: Names have to be all lowercase and match the regular expression ^[\w-]{3,32}$
    //    guildCommand.WithName("first-command");

    //    // Descriptions can have a max length of 100.
    //    guildCommand.WithDescription("This is my first guild slash command!");

    //    // Let's do our global command
    //    var globalCommand = new SlashCommandBuilder();
    //    globalCommand.WithName("first-global-command");
    //    globalCommand.WithDescription("This is my first global slash command");

    //    try
    //    {
    //        // Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
    //        await guild.CreateApplicationCommandAsync(guildCommand.Build());

    //        // With global commands we don't need the guild.
    //        await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
    //        // Using the ready event is a simple implementation for the sake of the example. Suitable for testing and development.
    //        // For a production bot, it is recommended to only run the CreateGlobalApplicationCommandAsync() once for each command.
    //    }
    //    catch (ApplicationCommandException exception)
    //    {
    //        // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
    //        var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

    //        // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
    //        Console.WriteLine(json);
    //    }
    //}
}