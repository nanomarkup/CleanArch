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

        public DtoMessageIntSendResponse Send(DtoMessageIntSendRequest dto)
        {
            var message = GetService<IMessageEntity>();
            message.Create(new DtoMessageEntity()
            {
                Sender = dto.Sender,
                Receiver = dto.Receiver,
                Text = dto.Text
            });

            return new DtoMessageIntSendResponse()
            {
                Result = message.Send()
            };
        }

        public IEnumerable<string> Read(DtoMessageIntReadRequest dto)
        {
            return Read(new DtoMessageIntReadByDateRequest()
            {
                Sender = dto.Sender,
                Receiver = dto.Receiver,
                Start = DateTime.MinValue,
                End = null
            });
        }

        public IEnumerable<string> Read(DtoMessageIntReadByDateRequest dto)
        {
            throw new NotImplementedException();
        }
    }
}
