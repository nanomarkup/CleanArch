using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        DtoUserInteractorId Create(DtoUserInteractorCreate dto);
        // Retrieve an user
        DtoUserInteractorInfo Retrieve(DtoUserInteractorId id);
        // Update an existing user by user id
        DtoUserInteractorId Modify(DtoUserInteractorModify dto);
        // Remove user by user id
        DtoUserInteractorId Delete(DtoUserInteractorId id);
    }

    public class DtoUserInteractorId
    {
        public Guid Id { get; set; }
    }

    public class DtoUserInteractorInfo
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserInteractorCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserInteractorModify
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
