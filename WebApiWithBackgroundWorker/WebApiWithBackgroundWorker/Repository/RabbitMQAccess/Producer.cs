using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using WebApiWithBackgroundWorker.Repository.Interface;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.RabbitMQAccess
{
    public class Producer : IProducer
    {
        private readonly ChannelWriter<Message> _writer;
        private readonly ILogger<Producer> _logger;

        public Producer(ChannelWriter<Message> writer, ILogger<Producer> logger)
        {
            _writer = writer;
            _logger = logger;
        }

        public async Task PublishAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _writer.WriteAsync(message, cancellationToken);
            _logger.LogInformation($"Producer > Published message {message.Id} '{message.Text}'");
        }
    }
}
