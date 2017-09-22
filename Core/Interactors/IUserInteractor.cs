using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        Guid Create(DtoIUserCreate dto);
        // Retrieve an user
        DtoIUserInfo Retrieve(Guid id);
        // Update an existing user by user id
        Guid Modify(DtoIUserModify dto);
        // Remove user by user id
        Guid Delete(Guid id);
    }

    public class DtoIUserInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoIUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoIUserModify
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
