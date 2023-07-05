using System.Threading;
using System.Threading.Tasks;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.Interface
{
    public interface IBackgroundSubscriberWorker
    {
       Task OnMessageAsync(object sender, RabbitSubscriberEventArgs args);
       Task ExecuteAsync(CancellationToken stoppingToken);

    }
}
