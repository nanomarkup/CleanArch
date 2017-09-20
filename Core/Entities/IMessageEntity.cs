using System;

namespace Core.Entities
{
    public class DtoMessageEntity
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public interface IMessageEntity : IDataEntity
    {
        // Owner/creator of message, read only
        Guid Sender { get; }
        // Receiver of message, read only
        Guid Receiver { get; }
        // Message
        string Text { get; set; }
        // Create a new message
        Guid Create(DtoMessageEntity messageEntity);
        // Initialize/load the message
        void Initialize(DtoDataEntity dataEntity, DtoMessageEntity messageEntity);
        // Send message to receiver
        bool Send();
    }
}
