using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        Guid Create(DtoIUserCreate dto);
        // Update an existing user by user id
        void Modify(DtoIUserModify dto);
        // Remove user by user id
        bool Delete(DtoIUserDelete dto);
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

    public class DtoIUserDelete
    {
        public Guid Id { get; set; }
    }
}
