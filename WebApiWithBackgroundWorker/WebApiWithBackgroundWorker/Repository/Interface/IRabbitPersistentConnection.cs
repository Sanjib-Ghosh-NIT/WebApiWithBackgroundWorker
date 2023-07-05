using RabbitMQ.Client;

namespace WebApiWithBackgroundWorker.Repository.Interface
{
    public interface IRabbitPersistentConnection
    {
        bool IsConnected { get; }
        IModel CreateChannel();
    }
}
