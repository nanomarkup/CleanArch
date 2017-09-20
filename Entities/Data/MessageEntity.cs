using System;
using Core.Entities;

namespace Entities
{
    public class MessageEntity : DataEntity, IMessageEntity
    {
        Guid sender;
        Guid receiver;
        public Guid Sender { get { return sender; } }
        public Guid Receiver { get { return receiver; } }
        public string Text
        {
            get { return this.Text; }
            set
            {
                this.Text = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Text is empty.", nameof(Text)) : value;
                Changed(nameof(Text));
            }
        }

        public Guid Create(DtoMessageEntity messageEntity)
        {
            base.Create();
            Initialize(messageEntity);
            return Id.Value;
        }

        public void Initialize(DtoDataEntity dataEntity, DtoMessageEntity messageEntity)
        {
            base.Initialize(dataEntity);
            Initialize(messageEntity);            
        }

        public bool Send()
        {            
            Changed(nameof(Send));
            return true;
        }

        void Initialize(DtoMessageEntity messageEntity)
        {
            this.sender = (messageEntity.Sender == Guid.Empty) ? throw new ArgumentException("GUID value is empty.", nameof(messageEntity.Sender)) 
                : messageEntity.Sender;
            this.receiver = (messageEntity.Receiver == Guid.Empty) ? throw new ArgumentException("GUID value is empty.", nameof(messageEntity.Receiver)) 
                : messageEntity.Receiver;
            Text = messageEntity.Text;
        }
    }
}
