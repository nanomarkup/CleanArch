using System;

namespace Core.Entities
{   
    public interface IMessageEntity : IBaseEntity
    {
        // Owner/creator of message, read only
        Guid Sender { get; }
        // Receiver of message, read only
        Guid Receiver { get; }
        // Message
        string Text { get; set; }
        // Identity entity
        IIdentityEntity Identity { get; }
        // Create a new message
        Guid Create(DtoEMessage dto);
        // Initialize/load the message
        void Initialize(DtoEMessageIdentity dto);
        // Send message to receiver
        Guid Send();
    }

    public class DtoEMessage
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoEMessageIdentity
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
        public DtoEIdentity Identity { get; set; }
    }
}
