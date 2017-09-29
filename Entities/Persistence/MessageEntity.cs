using System;
using Core.Entities;

namespace Entities
{
    public class MessageEntity : BaseEntity, IMessageEntity
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
                NotifyChanges(nameof(Text));
            }
        }

        public IIdentityEntity Identity { get; }

        public MessageEntity(IIdentityEntity identity)
        {
            Identity = identity;
            identity.Changed += (sender, args) =>
            {
                NotifyChanges(nameof(IIdentityEntity));
            };
        }

        public Guid Create(DtoMessageEntity dto)
        {
            sender = (dto.Sender == Guid.Empty) ?
                throw new ArgumentException("GUID value is empty.", nameof(dto.Sender)) : dto.Sender;
            receiver = (dto.Receiver == Guid.Empty) ?
                throw new ArgumentException("GUID value is empty.", nameof(dto.Receiver)) : dto.Receiver;            
            Text = dto.Text;
            isInitialized = true;
            return Identity.Create();
        }

        public void Initialize(DtoMessageIdentityEntity dto)
        {
            sender = (dto.Sender == Guid.Empty) ?
                throw new ArgumentException("GUID value is empty.", nameof(dto.Sender)) : dto.Sender;
            receiver = (dto.Receiver == Guid.Empty) ?
                throw new ArgumentException("GUID value is empty.", nameof(dto.Receiver)) : dto.Receiver;
            Text = dto.Text;
            isInitialized = true;
            Identity.Initialize(dto.Identity);
        }
    }
}
