using Core.Gateways;
using Core.Models;
using Infrastructure.Context;
using System;
using System.Linq;

namespace Infrastructure.Gateways
{
    public class MessageGateway : IMessageGateway
    {
        IInfrastructureDbContext context;

        public MessageGateway(IInfrastructureDbContext context)
        {
            this.context = context;
        }

        public Guid Add(MessageModel model)
        {
            var id = context.Messages.Add(model).Entity.Id;
            context.SaveChanges();
            return id;
        }

        public MessageModel Retrieve(Guid id)
        {
            return context.Messages.Find(id);
        }

        public IQueryable<MessageModel> Retrieve(DtoMessageGatewayQuery dto)
        {
            return context.Messages.Where(x => x.Sender == dto.Sender && x.Receiver == dto.Receiver);           
        }

        public Guid Modify(DtoMessageGatewayModify dto)
        {
            return Guid.Empty;
        }

        public Guid Delete(Guid id)
        {
            return Guid.Empty;
        }
    }
}
