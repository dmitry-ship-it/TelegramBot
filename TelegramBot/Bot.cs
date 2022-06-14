using Microsoft.Extensions.Logging;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Commands;
using TelegramBot.Configs;

namespace TelegramBot
{
    public sealed class Bot
    {
        private static Bot? _instance;
        private readonly ITelegramBotClient _bot;
        private readonly ILogger<Bot> _logger;

        private Bot()
        {
            _bot = new TelegramBotClient(Configuration.Instance.BotToken!);
            _logger = new Logging.Logger<Bot>(() => new Logging.LoggerConfiguration());
        }

        public static Bot Instance => _instance ??= new();

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // update handling
            if (update?.Message?.Chat is null)
            {
                return;
            }

            try
            {
                _logger.LogInformation($"Message from {update.Message.From}. Content: {update.Message.Text ?? "null"}");

                #if DEBUG
                    _logger.LogDebug(JsonSerializer.Serialize(update));
                #endif

                var command = Command.Create(update.Message.Text);

                if (command.ReplySticker is null)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat!,
                        text: command.ReplyMessage,
                        parseMode: ParseMode.Html,
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    await botClient.SendStickerAsync(
                        chatId: update.Message.Chat!,
                        sticker: command.ReplySticker,
                        replyToMessageId: update.Message.MessageId,
                        cancellationToken: cancellationToken);
                }

                _logger.LogInformation($"Command {command.GetType()} executed successfully.");
            }
            catch (ApiRequestException ex)
            {
                _logger.LogError($"Telegram API request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"{ex.GetType()}: {ex.Message}");
            }
        }

        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // remove creating new task after adding something awaitable there 
            await Task.Factory.StartNew(() =>
                _logger.LogError($"{exception.GetType()}: {exception.Message}"), cancellationToken);
        }

        public void Start(Action<ReceiverOptions>? options = null, CancellationToken cancellationToken = default)
        {
            var receiverOptions = new ReceiverOptions();
            options?.Invoke(receiverOptions);

            _bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        }
    }
}
