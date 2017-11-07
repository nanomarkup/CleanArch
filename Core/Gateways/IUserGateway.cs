using Core.Models;
using System;

namespace Core.Gateways
{
    public interface IUserGateway : IPersistenceGateway<UserModel, DtoUserModifiedGateway, DtoUserQueryGateway> { }

    public class DtoUserModifiedGateway
    {
        public Guid Id { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserQueryGateway
    {
        
    }
}
