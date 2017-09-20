using System;

namespace Core.Interactors
{
    public interface IUserInteractor
    {
        // Create a new user
        Guid Create(string firstName, string lastName, string email);
        // Update an existing user by user id
        void Update(Guid id, string firstName, string lastName, string email);
        // Remove user by user id
        bool Delete(Guid id);
    }
}
