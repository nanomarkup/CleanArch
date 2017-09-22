using System;
using System.Linq;

namespace Core.Gateways
{
    public interface IUserGateway : IPersistenceGateway<DtoGUserInfo, DtoGUserModified, DtoGUserQuery> { }
    
    public class DtoGUserInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoGUserModified
    {
        public Guid Id { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoGUserQuery
    {
        
    }
}
