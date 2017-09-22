using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        Guid Create(DtoUserIntCreate dto);
        // Update an existing user by user id
        void Modify(DtoUserIntModify dto);
        // Remove user by user id
        bool Delete(DtoUserIntDelete dto);
    }

    public class DtoUserIntCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserIntModify
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class DtoUserIntDelete
    {
        public Guid Id { get; set; }
    }
}
