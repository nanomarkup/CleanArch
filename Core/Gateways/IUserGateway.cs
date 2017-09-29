using System;
using System.Linq;

namespace Core.Gateways
{
    public interface IUserGateway : IPersistenceGateway<DtoUserInfoGateway, DtoUserModifiedGateway, DtoUserQueryGateway> { }
    
    public class DtoUserInfoGateway
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

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
