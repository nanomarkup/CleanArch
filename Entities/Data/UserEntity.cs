using System;
using Core.Entities;

namespace Entities
{
    public class UserEntity : DataEntity, IUserEntity
    {        
        public string FirstName
        {
            get { return this.FirstName; }
            set
            {
                this.FirstName = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("First Name is empty.", nameof(FirstName)) : value;
                Changed(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return this.LastName; }
            set
            {
                this.LastName = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Last Name is empty.", nameof(LastName)) : value;
                Changed(nameof(LastName));
            }
        }

        public string Email
        {
            get { return this.Email; }
            set
            {
                this.Email = (string.IsNullOrEmpty(value)) ? throw new ArgumentException("Email address is empty.", nameof(Email)) : value;
                Changed(nameof(Email));
            }
        }

        public Guid Create(DtoUserEntity userEntity)
        {
            base.Create();
            Initialize(userEntity);
            return Id.Value;
        }

        public void Initialize(DtoDataEntity dataEntity, DtoUserEntity userEntity)
        {
            base.Initialize(dataEntity);
            Initialize(userEntity);
        }

        void Initialize(DtoUserEntity userEntity)
        {
            FirstName = userEntity.FirstName;
            LastName = userEntity.LastName;
            Email = userEntity.Email;
        }
    }
}
