using Core.Entities;
using Core.Models;
using System;

namespace Entities
{
    public class MessageEntity : BaseEntity<IMessageModel>, IMessageEntity
    {
        public override void Validate(IMessageModel attrs)
        {
            if (attrs.Id == Guid.Empty)
                throw new ArgumentException("GUID value is empty.", nameof(attrs.Id));
            if (attrs.Modified < attrs.Created)
                throw new ArgumentException("The modified date less than the created date.", nameof(attrs.Modified));
            if (attrs.Sender == Guid.Empty) 
                throw new ArgumentException("GUID value is empty.", nameof(attrs.Sender));
            if (attrs.Receiver == Guid.Empty) 
                throw new ArgumentException("GUID value is empty.", nameof(attrs.Receiver));
            if (string.IsNullOrEmpty(attrs.Text))
                throw new ArgumentException("String value is empty.", nameof(attrs.Text));
        }
    }    
}
