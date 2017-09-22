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
        Guid Create(DtoMessageEntity dto);
        // Initialize/load the message
        void Initialize(DtoMessageIdentity dto);
        // Send message to receiver
        bool Send();
    }

    public class DtoMessageEntity
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoMessageIdentity
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
        public DtoIdentityEntity Identity { get; set; }
    }
}
