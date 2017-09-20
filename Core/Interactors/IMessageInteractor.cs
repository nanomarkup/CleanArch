using System;
using System.Collections.Generic;

namespace Core.Interactors
{
    public interface IMessageInteractor
    {
        // Send the text message to a receiver
        bool Send(Guid sender, Guid receiver, string text);
        // Read all messages sent between sender and receiver
        IEnumerable<string> Read(Guid sender, Guid receiver);
        // Read all messages sent between sender and receiver since some date or period of date
        IEnumerable<string> Read(Guid sender, Guid receiver, DateTime start, DateTime? end);
    }
}
