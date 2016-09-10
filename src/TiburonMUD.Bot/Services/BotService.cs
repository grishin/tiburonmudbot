using System;
using System.Threading.Tasks;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TiburonMUD.Engine;
using TiburonMUD.Engine.Models;
using System.Collections.Generic;
using System.Linq;

namespace TiburonMUD.Bot.Services
{
    public interface IBotService
    {
        void Start(string token);
        void Stop();
    }

    public class BotService : IBotService
    {
        private readonly ILogger _messageLogger;
        private readonly ILogger _exceptionLogger;

        private TelegramBotClient _bot;

        private World _world;
        private List<Player> _players = new List<Player>();

        public BotService()
        {
            _messageLogger = LogManager.GetLogger("message");
            _exceptionLogger = LogManager.GetCurrentClassLogger();

            _world = new WorldBuilder().Build();    
        }

        public void Start(string token)
        {
            _bot = new TelegramBotClient(token);
            _bot.OnMessage += BotOnMessageReceived;
            _bot.StartReceiving();
        }

        public void Stop()
        {
            _bot.StopReceiving();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            try
            {
                var message = messageEventArgs.Message;
                if (message == null || message.Type != MessageType.TextMessage) return;

                _messageLogger.Info($"{message.Chat.Id};{message.Chat.Title};{message.From.Id};{message.From.Username};{message.Text}");

                await ProcessMessage(message);
            }
            catch (Exception e)
            {
                _exceptionLogger.Fatal("Fatal error: " + e.Message + " " + (e.InnerException?.Message ?? ""));
            }
        }

        private async Task ProcessMessage(Message message)
        {
            var messageText = message.Text.ToLowerInvariant();

            if (messageText == "l" || messageText == "look")
            {
                await LookCommand(message);
            }
            else if (messageText == "n" || messageText == "north")
            {
                await MoveCommand(message, Direction.North);
            }
            else if (messageText == "s")
            {
                await MoveCommand(message, Direction.South);
            }
            else if (messageText == "w")
            {
                await MoveCommand(message, Direction.West);
            }
            else if (messageText == "e")
            {
                await MoveCommand(message, Direction.East);
            }
            else if (messageText == "u")
            {
                await MoveCommand(message, Direction.Up);
            }
            else if (messageText == "d")
            {
                await MoveCommand(message, Direction.Down);
            }
            else if (messageText == "who")
            {
                await  WhoCommand(message);
            }
            else if (messageText == "help")
            {
                await HelpCommand(message);
            }
        }

        private Player GetPlayerById(string id)
        {
            var player = _players.Where(x => x.Name == id).FirstOrDefault();
            if (player == null)
            {
                player = new Player(id, _world, "R_Entrance");
                _players.Add(player);
            }

            return player;
        }

        private async Task LookCommand(Message message)
        {
            var player = GetPlayerById(message.From.Username);

            string responseText = $"{player.CurrentRoom.Name.ToUpperInvariant()}\r\n{player.CurrentRoom.Description}";
            await _bot.SendTextMessageAsync(message.Chat.Id, responseText);
        }

        private async Task WhoCommand(Message message)
        {
            string responseText = $"Online: " + string.Join(" ", _players.Select(x=>x.Name + " => " + x.CurrentRoom.Id));
            await _bot.SendTextMessageAsync(message.Chat.Id, responseText);
        }

        private async Task HelpCommand(Message message)
        {
            string responseText = @"Welcome to Tiburon MUD. Available commands: 
l or look - look around
n - go north
s - go south
e - go east
w - go west
u - go up
d - go down
who - who is online
help - this text
";
            await _bot.SendTextMessageAsync(message.Chat.Id, responseText);
        }


        private async Task MoveCommand(Message message, Direction direction)
        {
            var player = GetPlayerById(message.From.Username);

            if (player.Move(direction))
            {
                await LookCommand(message);
            }
            else
            {
                await _bot.SendTextMessageAsync(message.Chat.Id, "You can't go that way.");
            }
        }
    }
}
