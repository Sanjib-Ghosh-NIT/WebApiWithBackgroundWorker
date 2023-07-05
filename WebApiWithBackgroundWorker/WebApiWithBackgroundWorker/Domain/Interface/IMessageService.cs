using System;
using System.Collections.Generic;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Domain.Interface
{
    public interface IMessageService
    {
        IReadOnlyCollection<Message> GetMessages();
    }
}
