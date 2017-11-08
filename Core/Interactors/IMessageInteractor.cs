using System;
using System.Collections.Generic;

namespace Core.Interactors
{
    public interface IMessageInteractor
    {
        // Send the text message to a receiver
        DtoMessageInteractorId Send(DtoMessageInteractorSend dto);
        // Read a message by message Id
        DtoMessageInteractorInfo Read(DtoMessageInteractorReadById dto);
        // Read all messages sent between sender and receiver
        IEnumerable<DtoMessageInteractorInfo> Read(DtoMessageInteractorRead dto);        
        // Read all messages sent between sender and receiver since some date or period of date
        IEnumerable<DtoMessageInteractorInfo> Read(DtoMessageInteractorReadByDate dto);
    }

    public class DtoMessageInteractorId
    {
        public Guid Id { get; set; }
    }

    public class DtoMessageInteractorInfo
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

    public class DtoMessageInteractorSend
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoMessageInteractorRead
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
    }

    public class DtoMessageInteractorReadById
    {
        public Guid Id { get; set; }
    }

    public class DtoMessageInteractorReadByDate
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
