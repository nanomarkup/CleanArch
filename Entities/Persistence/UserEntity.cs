using Core.Entities;
using Core.Models;
using System;

namespace Entities
{   
    public class UserEntity : BaseEntity<IUserModel>, IUserEntity
    {
        public override void Validate(IUserModel attrs)
        {
            if (attrs.Id == Guid.Empty)
                throw new ArgumentException("GUID value is empty.", nameof(attrs.Id));
            if (attrs.Modified < attrs.Created)
                throw new ArgumentException("The modified date less than the created date.", nameof(attrs.Modified));
            if (string.IsNullOrEmpty(attrs.FirstName)) 
                throw new ArgumentException("String value is empty.", nameof(attrs.FirstName));
            if (string.IsNullOrEmpty(attrs.LastName))
                throw new ArgumentException("String value is empty.", nameof(attrs.LastName));
            if (string.IsNullOrEmpty(attrs.Email))
                throw new ArgumentException("String value is empty.", nameof(attrs.Email));
        }     
    }
}
