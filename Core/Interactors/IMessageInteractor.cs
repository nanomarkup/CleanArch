using System;
using System.Collections.Generic;

namespace Core.Interactors
{
    public interface IMessageInteractor
    {
        // Send the text message to a receiver
        DtoMessageIntSendResponse Send(DtoMessageIntSendRequest dto);
        // Read all messages sent between sender and receiver
        IEnumerable<string> Read(DtoMessageIntReadRequest dto);
        // Read all messages sent between sender and receiver since some date or period of date
        IEnumerable<string> Read(DtoMessageIntReadByDateRequest dto);
    }

    public class DtoMessageIntSendRequest
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoMessageIntSendResponse
    {
        public bool Result { get; set; }
    }

    public class DtoMessageIntReadRequest
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
    }

    public class DtoMessageIntReadByDateRequest
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
