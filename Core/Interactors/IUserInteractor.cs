using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        DtoIUserId Create(DtoIUserCreate dto);
        // Retrieve an user
        DtoIUserInfo Retrieve(DtoIUserId id);
        // Update an existing user by user id
        DtoIUserId Modify(DtoIUserModify dto);
        // Remove user by user id
        DtoIUserId Delete(DtoIUserId id);
    }

    public class DtoIUserId
    {
        public Guid Id { get; set; }
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
