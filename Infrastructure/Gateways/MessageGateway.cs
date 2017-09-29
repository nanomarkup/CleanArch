using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Gateways;
using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Gateways
{
    public class MessageGateway : IMessageGateway
    {
        IInfrastructureDbContext context;

        public MessageGateway(IInfrastructureDbContext context)
        {
            this.context = context;
        }

        public Guid Add(DtoMessageInfoGateway dto)
        {
            var id = context.Messages.Add(Mapper.Map<MessageEntity>(dto)).Entity.Id;
            context.SaveChanges();
            return id;
        }

        public DtoMessageInfoGateway Retrieve(Guid id)
        {
            return Mapper.Map<DtoMessageInfoGateway>(context.Messages.Find(id));
        }

        public IQueryable<DtoMessageInfoGateway> Retrieve(DtoMessageQueryGateway dto)
        {
            var entities = context.Messages.Where(x => x.Sender == dto.Sender && x.Receiver == dto.Receiver);
            if (entities == null)
                return new List<DtoMessageInfoGateway>().AsQueryable();
            else
                return entities.Select(x => Mapper.Map<DtoMessageInfoGateway>(x));
        }

        public Guid Modify(DtoMessageModifiedGateway dto)
        {
            return Guid.Empty;
        }

        public Guid Delete(Guid id)
        {
            return Guid.Empty;
        }
    }
}
