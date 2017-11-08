using Core.Models;
using System;

namespace Core.Gateways
{
    public interface IUserGateway : IPersistenceGateway<UserModel, DtoUserGatewayModify, DtoUserGatewayQuery> { }

    public class DtoUserGatewayModify
    {
        public Guid Id { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserGatewayQuery
    {
        
    }
}
