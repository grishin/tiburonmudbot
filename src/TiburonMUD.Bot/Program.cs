using System;
using System.Configuration;
using NLog;
using SimpleInjector;
using TiburonMUD.Bot.Services;
using Topshelf;

namespace TiburonMUD.Bot
{
    class Program
    {
        static string TelegramApiKey { get; set; }

        static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(name => new Program());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Office DUNGEON in Telegram");
                x.SetDisplayName("Tiburon MUD Bot");
                x.SetServiceName("Tiburon MUD Bot");

                x.UseNLog();
                x.StartAutomatically();
                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(0);
                });

                // getting telegram bot api key
                x.AddCommandLineDefinition("telegramapikey", val => TelegramApiKey = val);
                if (string.IsNullOrEmpty(TelegramApiKey))
                {
                    TelegramApiKey = ConfigurationManager.AppSettings["TelegramApiKey"];
                }
            });
        }

        private readonly ILogger _logger;
        private readonly IBotService _bot;

        public Program()
        {
            var serviceProvider = ConfigureServices();

            _logger = serviceProvider.GetInstance<ILogger>();
            _bot = serviceProvider.GetInstance<IBotService>();
        }

        public void Start()
        {
            try
            {
                _logger.Info("Starting");

                if (string.IsNullOrEmpty(TelegramApiKey))
                {
                    _logger.Info("TelegramApiKey is not defined");
                    return;
                }

                _bot.Start(TelegramApiKey);
                _logger.Info("Bot is listening");
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                _logger.Fatal(e, "Fatal exception in Start()");
            }
        }

        public void Stop()
        {
            try
            {
                _bot.Stop();
                _logger.Info("Bot is stopped");
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                _logger.Fatal(e, "Fatal exception in Stop()");
            }
        }

        private static Container ConfigureServices()
        {
            var container = new Container();

            container.Register<ILogger>(LogManager.GetCurrentClassLogger, Lifestyle.Singleton);
            container.Register<IBotService, BotService>(Lifestyle.Singleton);

            container.Verify();
            return container;
        }
    }
}
