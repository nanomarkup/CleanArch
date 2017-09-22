using System;

namespace Core.Gateways
{
    public interface IUserGateway : IPersistenceGateway<DtoGUserInfo, DtoGUserRetrieve, DtoGUserInfo, DtoGUserDelete> { }
    
    public class DtoGUserInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoGUserRetrieve
    { }

    public class DtoGUserDelete
    {
        public Guid Id { get; set; }
    }
}
