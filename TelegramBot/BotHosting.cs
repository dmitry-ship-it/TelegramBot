using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TelegramBot.Commands;
using TelegramBot.Commands.Abstract;
using NLog.Extensions.Logging;
using TelegramBot.Configs;

namespace TelegramBot
{
    public static class BotHosting
    {
        private static IHost? _host;

        public static IServiceProvider ServiceProvider { get; private set; } = default!;

        public static async Task Run(string[] args)
        {
            _host = SetupHost(args);
            await _host.RunAsync();
        }

        private static IHost SetupHost(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(RegisterServices)
                .Build();

            ServiceProvider = host.Services;

            return host;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.ClearProviders();
                options.SetMinimumLevel(LogLevel.Information);
                options.AddNLog();
            });

            // add sevices there
            services.AddSingleton<ICommandFactory, CommandFactory>();

            // services.AddSingleton<ICommand, CommandFromDictionary>();
            services.AddSingleton<ICommand, QuestionCommand>();
            services.AddSingleton<ICommand, ReplyCommand>();
            services.AddSingleton<ICommand, RollCommand>();
            services.AddSingleton<ICommand, ScheduleCommand>();
            services.AddSingleton(Configuration.LoadConfiguration());

            services.AddHostedService<Bot>();
        }
    }
}
