using System;


namespace WebApiWithBackgroundWorker.Repository.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }
    }
}
