using System;
using System.Linq;
using System.Collections.Generic;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;
using AutoMapper;

namespace Interactors
{
    public class MessageInteractor : BaseInteractor, IMessageInteractor
    {
        public MessageInteractor(IServiceProvider provider) : base(provider) { }

        public DtoIMessageId Send(DtoIMessageSend dto)
        {
            var message = GetService<IMessageEntity>();
            message.Create(Mapper.Map<DtoEMessage>(dto));
            return new DtoIMessageId() { Id = message.Send() };
        }

        public IEnumerable<DtoIMessageInfo> Read(DtoIMessageRead dto)
        {
            return Read(Mapper.Map<DtoIMessageReadByDate>(dto));
        }

        public IEnumerable<DtoIMessageInfo> Read(DtoIMessageReadByDate dto)
        {
            var gw = GetService<IMessageGateway>();
            var messages = gw.Retrieve(Mapper.Map<DtoGMessageQuery>(dto));
            return messages.ToList().Select(x => Mapper.Map<DtoIMessageInfo>(x));
        }
    }
}
