using System;


namespace WebApiWithBackgroundWorker.Repository.Models
{
    public class RabbitSubscriberEventArgs : EventArgs
     {
        public RabbitSubscriberEventArgs(Message message)
        {
            this.Message = message;
        }

        public Message Message { get; }
    }
}
