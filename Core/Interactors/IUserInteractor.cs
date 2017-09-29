using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        DtoUserIdInteractor Create(DtoUserCreateInteractor dto);
        // Retrieve an user
        DtoUserInfoInteractor Retrieve(DtoUserIdInteractor id);
        // Update an existing user by user id
        DtoUserIdInteractor Modify(DtoUserModifyInteractor dto);
        // Remove user by user id
        DtoUserIdInteractor Delete(DtoUserIdInteractor id);
    }

    public class DtoUserIdInteractor
    {
        public Guid Id { get; set; }
    }

    public class DtoUserInfoInteractor
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserCreateInteractor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserModifyInteractor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
