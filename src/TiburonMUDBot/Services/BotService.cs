using System;
using System.Threading.Tasks;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TiburonMUD.Engine;
using TiburonMUD.Engine.Models;

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
        private Player _player;

        public BotService()
        {
            _messageLogger = LogManager.GetLogger("message");
            _exceptionLogger = LogManager.GetCurrentClassLogger();

            _world = new WorldBuilder().Build();
            _player = new Player(_world, "R_Entrance");
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

            if (messageText == "l")
            {
                await LookCommand(message);
            }
            else if (messageText == "n")
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
        }

        private async Task LookCommand(Message message)
        {
            string responseText = $"{_player.CurrentRoom.Name.ToUpperInvariant()}\r\n{_player.CurrentRoom.Description}";
            await _bot.SendTextMessageAsync(message.Chat.Id, responseText);
        }

        private async Task MoveCommand(Message message, Direction direction)
        {
            if (_player.Move(direction))
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
