using System;

namespace Core.Entities
{      
    public interface IUserEntity : IBaseEntity
    {
        // User first name
        string FirstName { get; set; }
        // User last name
        string LastName { get; set; }
        // User email
        string Email { get; set; }
        // Identity entity
        IIdentityEntity Identity { get; }
        // Create a new user
        Guid Create(DtoUserCreate dto);
        // Initialize/load the user
        void Initialize(DtoUserEntity dto);
    }

    public class DtoUserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DtoIdentityEntity Identity { get; set; }
    }

    public class DtoUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
