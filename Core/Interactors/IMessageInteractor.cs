using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Interactors
{
    public interface IMessageInteractor
    {
        // Send the text message to a receiver
        DtoIMessageId Send(DtoIMessageSend dto);
        // Read all messages sent between sender and receiver
        IEnumerable<DtoIMessageInfo> Read(DtoIMessageRead dto);
        // Read all messages sent between sender and receiver since some date or period of date
        IEnumerable<DtoIMessageInfo> Read(DtoIMessageReadByDate dto);
    }

    public class DtoIMessageId
    {
        public Guid Id { get; set; }
    }

    public class DtoIMessageInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoIMessageSend
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoIMessageRead
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
    }

    public class DtoIMessageReadByDate
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
