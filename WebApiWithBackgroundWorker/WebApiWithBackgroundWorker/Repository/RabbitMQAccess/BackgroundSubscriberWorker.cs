using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiWithBackgroundWorker.Repository.Interface;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.RabbitMQAccess
{
    public class BackgroundSubscriberWorker : BackgroundService
    {
        private readonly IRabbitSubscriber _subscriber;
        private readonly ILogger<BackgroundSubscriberWorker> _logger;

        private readonly IProducer _producer;
        private readonly IEnumerable<IConsumer> _consumers;

        public BackgroundSubscriberWorker(IRabbitSubscriber subscriber, IProducer producer, IEnumerable<IConsumer> consumers, ILogger<BackgroundSubscriberWorker> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            _subscriber.OnMessage += OnMessageAsync;

            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
            _consumers = consumers ?? Enumerable.Empty<Consumer>();
        }

        private async Task OnMessageAsync(object sender, RabbitSubscriberEventArgs args)
        {
            _logger.LogInformation($"Got a new message: {args.Message.Text} at {args.Message.CreationDate}");         

            await _producer.PublishAsync(args.Message);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Start();

            var consumerTasks = _consumers.Select(c => c.BeginConsumeAsync(stoppingToken));
            await Task.WhenAll(consumerTasks);
        }
    }
}
