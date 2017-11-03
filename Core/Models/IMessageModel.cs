using System;

namespace Core.Models
{
    // An interface to support data accessibility
    public interface IMessageModel : IPoco
    {
        // Primary key, read only  
        Guid Id { get; }
        // Created date, read only
        DateTime Created { get; }
        // Modified date, read only
        DateTime Modified { get; }
        // Owner/creator of message, read only
        Guid Sender { get; }
        // Receiver of message, read only
        Guid Receiver { get; }
        // Message
        string Text { get; set; }
    }

    // POCO object to support data persistence
    public class MessageModel : Poco, IMessageModel
    {        
        // Primary key 
        public Guid Id { get; set; }
        // Created date
        public DateTime Created { get; set; }
        // Modified date
        public DateTime Modified { get; set; }
        // Owner/creator of message
        public Guid Sender { get; set; }
        // Receiver of message
        public Guid Receiver { get; set; }
        // Message
        [PropertyChanged]
        public string Text { get; set; }
    }    
}
