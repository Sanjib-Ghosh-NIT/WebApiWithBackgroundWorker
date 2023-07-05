using System;
using System.Collections.Generic;
using WebApiWithBackgroundWorker.Repository.Interface;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Repository.InMemoryData
{
    public class InMemoryMessagesRepository : IInMemoryMessagesRepository
    {
        private readonly Queue<Message> _messages;

        public InMemoryMessagesRepository()
        {
            _messages = new Queue<Message>();
        }

        public void Add(Message message)
        {
            _messages.Enqueue(message ?? throw new ArgumentNullException(nameof(message)));
        }

        public IReadOnlyCollection<Message> GetMessages()
        {
            return _messages.ToArray();
        }
    }
}
