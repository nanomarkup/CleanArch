using AutoMapper;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interactors
{
    public class MessageInteractor : BaseInteractor, IMessageInteractor
    {
        public MessageInteractor(IServiceProvider provider) : base(provider) { }

        public DtoMessageIdInteractor Send(DtoMessageSendInteractor dto)
        {
            var model = Mapper.Map<MessageModel>(dto);
            model.Id = Guid.NewGuid();
            model.Created = DateTime.Now;
            model.Modified = model.Created;

            var message = GetService<IMessageEntity<IMessageModel>>();
            message.Changed += (sender, args) => { GetService<IMessageGateway>().Add(Mapper.Map<DtoMessageInfoGateway>(message.Attrs)); };
            message.Create(model);
            return new DtoMessageIdInteractor() { Id = message.Attrs.Id };
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
