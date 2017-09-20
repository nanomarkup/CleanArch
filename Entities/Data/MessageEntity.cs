using System;
using Core.Entities;

namespace Entities
{
    public class MessageEntity : DataEntity, IMessageEntity
    {
        Guid sender;
        Guid receiver;
        string text;
        public Guid Sender { get { return sender; } }
        public Guid Receiver { get { return receiver; } }
        public string Text
        {
            get { return text; }
            set
            {
                text = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Text is empty.", nameof(Text)) : value;
                Changed(nameof(Text));
            }
        }

        public Guid Create(DtoMessageEntity messageEntity)
        {            
            Initialize(messageEntity);
            base.Create();
            return Id.Value;
        }

        public void Initialize(DtoDataEntity dataEntity, DtoMessageEntity messageEntity)
        {            
            Initialize(messageEntity);
            base.Initialize(dataEntity);
        }

        public bool Send()
        {            
            Changed(nameof(Send));
            return true;
        }

        void Initialize(DtoMessageEntity messageEntity)
        {
            sender = (messageEntity.Sender == Guid.Empty) ? throw new ArgumentException("GUID value is empty.", nameof(messageEntity.Sender)) 
                : messageEntity.Sender;
            receiver = (messageEntity.Receiver == Guid.Empty) ? throw new ArgumentException("GUID value is empty.", nameof(messageEntity.Receiver)) 
                : messageEntity.Receiver;
            Text = messageEntity.Text;
        }
    }
}
