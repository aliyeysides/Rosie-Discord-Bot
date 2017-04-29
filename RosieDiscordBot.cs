using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Commands;

namespace RosieDiscordBot
{
    public class RosieBot
    {
        static void Main(string[] args)
        {
            new RosieBot().Start();
        }

        private DiscordClient _client;

        public void Start()
        {
            _client = new DiscordClient(x =>
            {
                x.AppName = "Rosie Discord Bot";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            var token = APIKeys.DiscordClientToken;

            CreateCommands();

            _client.ExecuteAndWait(async () =>
            {
                await _client.Connect(token, TokenType.Bot);
            });
        }

        public void CreateCommands()
        {
            var cService = _client.GetService<CommandService>();

            cService.CreateCommand("test")
                .Description("returns basic string")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Everything must be clean...very clean");
                });

            cService.CreateCommand("return")
                .Description("returns param")
                .Parameter("param", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    var toReturn = $"{e.GetArg("param")}";
                    await e.Channel.SendMessage(toReturn);
                });
        }

        public void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] [{e.Message}]");
        }
    }

}
