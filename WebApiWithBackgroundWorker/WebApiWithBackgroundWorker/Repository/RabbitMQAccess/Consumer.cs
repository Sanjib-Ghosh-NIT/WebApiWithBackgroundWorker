using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using WebApiWithBackgroundWorker.Repository.Interface;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.RabbitMQAccess
{
    public class Consumer : IConsumer
    {
        private readonly ChannelReader<Message> _reader;
        private readonly ILogger<Consumer> _logger;

        private readonly IInMemoryMessagesRepository _messagesRepository;
        private readonly int _instanceId;
        private static readonly Random Random = new Random();

        public Consumer(ChannelReader<Message> reader, 
                        ILogger<Consumer> logger,
                        int instanceId,
                        IInMemoryMessagesRepository messagesRepository)
        {
            _reader = reader;
            _instanceId = instanceId;
            _logger = logger;
            _messagesRepository = messagesRepository;
        }

        public async Task BeginConsumeAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.LogInformation($"CONSUMER {_instanceId} > Starting");
            _logger.LogInformation($"CONSUMER ({_instanceId})> Waiting to Receive new message");

            try
            {
                while (await _reader.WaitToReadAsync())
                {                   
                    if (_reader.TryRead(out var message))
                    {
                        _logger.LogInformation($"CONSUMER ({_instanceId})> Received message {message.Id} : {message.Text}");
                        _messagesRepository.Add(message);
                        _logger.LogInformation($"CONSUMER ({_instanceId})> Added to In-Memory storage");
                    }
                    _logger.LogInformation($"CONSUMER ({_instanceId})> Waiting to Receive new message");
                }


                //var message = await _reader.ReadAsync(cancellationToken);

                //   _logger.LogInformation($"CONSUMER ({_instanceId})> Received message {message.Id} : {message.Text}");
                //   await Task.Delay(500, cancellationToken);
                //   _messagesRepository.Add(message);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, $"Consumer {_instanceId} > forced stop");
            }

            _logger.LogInformation($"Consumer {_instanceId} > shutting down");
        }
   
}
}
