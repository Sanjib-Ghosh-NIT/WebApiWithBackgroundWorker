using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiWithBackgroundWorker.Repository.Interface
{
    public interface IConsumer
    {
        Task BeginConsumeAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
