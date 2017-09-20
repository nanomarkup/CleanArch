using System;
using Core.Entities;

namespace Entities
{
    public class UserEntity : DataEntity, IUserEntity
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
                Changed(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Last Name is empty.", nameof(LastName)) : value;
                Changed(nameof(LastName));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Email address is empty.", nameof(Email)) : value;
                Changed(nameof(Email));
            }
        }

        public Guid Create(DtoUserEntity userEntity)
        {
            Initialize(userEntity);
            base.Create();            
            return Id.Value;
        }

        public void Initialize(DtoDataEntity dataEntity, DtoUserEntity userEntity)
        {
            Initialize(userEntity);
            base.Initialize(dataEntity);            
        }

        void Initialize(DtoUserEntity userEntity)
        {
            FirstName = userEntity.FirstName;
            LastName = userEntity.LastName;
            Email = userEntity.Email;
        }
    }
}
