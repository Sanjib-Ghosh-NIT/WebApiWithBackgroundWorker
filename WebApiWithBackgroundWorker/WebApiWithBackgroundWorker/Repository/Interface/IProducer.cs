using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.Interface
{
    public interface IProducer
    {
        Task PublishAsync(Message message, CancellationToken cancellationToken = default(CancellationToken));
    }
}
