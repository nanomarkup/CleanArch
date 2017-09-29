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

        public DtoMessageIdInteractor Send(DtoMessageSendInteractor dto)
        {
            var message = GetService<IMessageEntity>();
            message.Changed += (sender, args) => { GetService<IMessageGateway>().Add(Mapper.Map<DtoMessageInfoGateway>(message)); };
            return new DtoMessageIdInteractor() { Id = message.Create(Mapper.Map<DtoMessageEntity>(dto)) };
        }

        public DtoMessageInfoInteractor Read(DtoMessageReadByIdInteractor dto)
        {
            return Mapper.Map<DtoMessageInfoInteractor>(GetService<IMessageGateway>().Retrieve(dto.Id));
        }

        public IEnumerable<DtoMessageInfoInteractor> Read(DtoMessageReadInteractor dto)
        {
            return Read(Mapper.Map<DtoMessageReadByDateInteractor>(dto));
        }        

        public IEnumerable<DtoMessageInfoInteractor> Read(DtoMessageReadByDateInteractor dto)
        {
            var gw = GetService<IMessageGateway>();
            var messages = gw.Retrieve(Mapper.Map<DtoMessageQueryGateway>(dto));
            if (messages == null)
                return new List<DtoMessageInfoInteractor>();
            else
                return messages.Select(x => Mapper.Map<DtoMessageInfoInteractor>(x));
        }
    }
}
