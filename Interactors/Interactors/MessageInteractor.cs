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

        public DtoMessageInteractorId Send(DtoMessageInteractorSend dto)
        {
            var model = Mapper.Map<MessageModel>(dto);
            model.Id = Guid.NewGuid();
            model.Created = DateTime.Now;
            model.Modified = model.Created;

            var message = GetService<IMessageEntity>();
            message.Changed += (sender, args) => { GetService<IMessageGateway>().Add(message.Attrs as MessageModel); };
            message.Create(model);
            return new DtoMessageInteractorId() { Id = message.Attrs.Id };
        }

        public DtoMessageInteractorInfo Read(DtoMessageInteractorReadById dto)
        {
            return Mapper.Map<DtoMessageInteractorInfo>(GetService<IMessageGateway>().Retrieve(dto.Id));
        }

        public IEnumerable<DtoMessageInteractorInfo> Read(DtoMessageInteractorRead dto)
        {
            return Read(Mapper.Map<DtoMessageInteractorReadByDate>(dto));
        }        

        public IEnumerable<DtoMessageInteractorInfo> Read(DtoMessageInteractorReadByDate dto)
        {
            var gw = GetService<IMessageGateway>();
            var messages = gw.Retrieve(Mapper.Map<DtoMessageGatewayQuery>(dto));
            if (messages == null)
                return new List<DtoMessageInteractorInfo>();
            else
                return messages.Select(x => Mapper.Map<DtoMessageInteractorInfo>(x));
        }
    }
}
