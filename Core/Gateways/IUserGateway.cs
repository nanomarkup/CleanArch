using System;

namespace Core.Gateways
{
    public interface IUserGateway : IPersistenceGateway<DtoUserGwInfo, DtoUserGwRetrieve, DtoUserGwInfo, DtoUserGwDelete> { }
    
    public class DtoUserGwInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserGwRetrieve
    { }

    public class DtoUserGwDelete
    {
        public Guid Id { get; set; }
    }
}
