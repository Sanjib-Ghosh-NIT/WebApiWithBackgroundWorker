using System.Collections.Generic;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.Interface
{
    public interface IInMemoryMessagesRepository
    {
        void Add(Message message);
        IReadOnlyCollection<Message> GetMessages();
    }
}
