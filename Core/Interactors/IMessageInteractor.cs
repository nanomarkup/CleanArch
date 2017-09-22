using System;
using System.Collections.Generic;

namespace Core.Interactors
{
    public interface IMessageInteractor
    {
        // Send the text message to a receiver
        DtoIMessageSendResponse Send(DtoIMessageSendRequest dto);
        // Read all messages sent between sender and receiver
        IEnumerable<string> Read(DtoIMessageReadRequest dto);
        // Read all messages sent between sender and receiver since some date or period of date
        IEnumerable<string> Read(DtoIMessageReadByDateRequest dto);
    }

    public class DtoIMessageSendRequest
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Text { get; set; }
    }

    public class DtoIMessageSendResponse
    {
        public bool Result { get; set; }
    }

    public class DtoIMessageReadRequest
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
    }

    public class DtoIMessageReadByDateRequest
    {
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
