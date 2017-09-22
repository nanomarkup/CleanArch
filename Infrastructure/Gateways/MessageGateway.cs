using System;
using System.Linq;
using Core.Gateways;

namespace Infrastructure.Gateways
{
    public class MessageGateway : IMessageGateway
    {
        public Guid Add(DtoGMessageInfo dto)
        {
            return Guid.Empty;
        }

        public DtoGMessageInfo Retrieve(Guid id)
        {
            return new DtoGMessageInfo();
        }

        public IQueryable<DtoGMessageInfo> Retrieve(DtoGMessageQuery dto)
        {
            return null;
        }

        public Guid Modify(DtoGMessageModified dto)
        {
            return Guid.Empty;
        }

        public Guid Delete(Guid id)
        {
            return Guid.Empty;
        }
    }
}
