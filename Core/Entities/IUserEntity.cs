using System;

namespace Core.Entities
{
    public class DtoUserEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public interface IUserEntity : IDataEntity
    {
        // User first name
        string FirstName { get; set; }
        // User last name
        string LastName { get; set; }
        // User email
        string Email { get; set; }
        // Create a new user
        Guid Create(DtoUserEntity userEntity);
        // Initialize/load the user
        void Initialize(DtoDataEntity dataEntity, DtoUserEntity userEntity);
    }
}
