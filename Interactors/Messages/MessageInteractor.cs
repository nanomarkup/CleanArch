using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities;
using Core.Interactors;

namespace Interactors
{
    public class MessageInteractor : BaseInteractor, IMessageInteractor
    {
        public MessageInteractor(IServiceProvider provider) : base(provider) { }

        public bool Send(Guid sender, Guid receiver, string text)
        {
            var message = GetService<IMessageEntity>();
            message.Create(new DtoMessageCreate()
            {
                Sender = sender,
                Receiver = receiver,
                Text = text
            });
            return message.Send();
        }

        public IEnumerable<string> Read(Guid sender, Guid receiver)
        {            
            return Read(sender, receiver, DateTime.MinValue, null);
        }

        public IEnumerable<string> Read(Guid sender, Guid receiver, DateTime start, DateTime? end)
        {
            throw new NotImplementedException();
        }
    }
}
