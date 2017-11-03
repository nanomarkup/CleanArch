using Core.Gateways;
using Infrastructure.Context;
using System;
using System.Linq;

namespace Infrastructure.Gateways
{
    public class UserGateway : IUserGateway
    {
        IInfrastructureDbContext context;

        public UserGateway(IInfrastructureDbContext context)
        {
            this.context = context;
        }

        public Guid Add(DtoUserInfoGateway dto)
        {
            return Guid.Empty;
        }

        public DtoUserInfoGateway Retrieve(Guid id)
        {
            return new DtoUserInfoGateway();
        }

        public IQueryable<DtoUserInfoGateway> Retrieve(DtoUserQueryGateway dto)
        {
            return null;
        }

        public Guid Modify(DtoUserModifiedGateway dto)
        {
            return Guid.Empty;
        }

        public Guid Delete(Guid id)
        {
            return Guid.Empty;
        }
    }
}