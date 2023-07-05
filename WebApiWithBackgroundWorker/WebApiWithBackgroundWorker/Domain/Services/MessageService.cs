using System.Collections.Generic;
using WebApiWithBackgroundWorker.Domain.Interface;
using WebApiWithBackgroundWorker.Repository.Interface;
using WebApiWithBackgroundWorker.Repository.Models;

namespace WebApiWithBackgroundWorker.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IInMemoryMessagesRepository _inMemoryMessagesRepository;

        public MessageService( IInMemoryMessagesRepository inMemoryMessagesRepository)
        {
            _inMemoryMessagesRepository = inMemoryMessagesRepository;
        }


        public IReadOnlyCollection<Message> GetMessages()
        {
            return _inMemoryMessagesRepository.GetMessages(); ;
        }
    }
}
