using Core.Gateways;
using Core.Models;
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

        public Guid Add(UserModel model)
        {
            return Guid.Empty;
        }

        public UserModel Retrieve(Guid id)
        {
            return new UserModel();
        }

        public IQueryable<UserModel> Retrieve(DtoUserGatewayQuery dto)
        {
            return null;
        }

        public Guid Modify(DtoUserGatewayModify dto)
        {
            return Guid.Empty;
        }

        public Guid Delete(Guid id)
        {
            return Guid.Empty;
        }
    }
}