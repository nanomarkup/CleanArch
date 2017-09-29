using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Interactors
{
    public interface IMessageInteractor
    {
        // Send the text message to a receiver
        DtoMessageIdInteractor Send(DtoMessageSendInteractor dto);
        // Read a message by message Id
        DtoMessageInfoInteractor Read(DtoMessageReadByIdInteractor dto);
        // Read all messages sent between sender and receiver
        IEnumerable<DtoMessageInfoInteractor> Read(DtoMessageReadInteractor dto);        
        // Read all messages sent between sender and receiver since some date or period of date
        IEnumerable<DtoMessageInfoInteractor> Read(DtoMessageReadByDateInteractor dto);
    }

    public class DtoMessageIdInteractor
    {
        public Guid Id { get; set; }
    }

    public class DtoMessageInfoInteractor
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Text}";
        }
    }

    public class DtoMessageSendInteractor
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoMessageReadInteractor
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
    }

    public class DtoMessageReadByIdInteractor
    {
        public Guid Id { get; set; }
    }

    public class DtoMessageReadByDateInteractor
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
