using System;
using Core.Entities;

namespace Entities
{
    public class UserEntity : BaseEntity, IUserEntity
    {
        string firstName;
        string lastName;
        string email;   

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("First Name is empty.", nameof(FirstName)) : value;
                NotifyChanges(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Last Name is empty.", nameof(LastName)) : value;
                NotifyChanges(nameof(LastName));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Email address is empty.", nameof(Email)) : value;
                NotifyChanges(nameof(Email));
            }
        }

        public IIdentityEntity Identity { get; }

        public UserEntity(IIdentityEntity identity)
        {
            Identity = identity;
            identity.Changed += (sender, args) =>
            {
                NotifyChanges(nameof(IIdentityEntity));
            };            
        }

        public Guid Create(DtoEUser dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Email = dto.Email;
            return Identity.Create();            
        }

        public void Initialize(DtoEUserIdentity dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Email = dto.Email;
            Identity.Initialize(dto.Identity);            
        } 
    }
}
