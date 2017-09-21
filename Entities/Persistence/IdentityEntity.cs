using System;
using Core.Entities;
using System.ComponentModel;

namespace Entities
{   
    public class IdentityEntity : BaseEntity, IIdentityEntity
    {
        Guid id;
        DateTime created;
        DateTime modified;                
        bool isInitialized;

        public Guid Id { get { return id; } }
        public DateTime Created { get { return created; } }
        public DateTime Modified { get { return modified; } }  

        public Guid Create()
        {
            if (isInitialized)
                throw new InvalidOperationException("The object has been initialized.");

            id = Guid.NewGuid();
            created = DateTime.Now;
            modified = Created;
            // Mark object as initialized
            isInitialized = true;
            // Perform Changed event
            NotifyChanges(nameof(Create));
            // Return primary key
            return Id;
        }

        public void Initialize(DtoIdentityEntity dto)
        {
            if (isInitialized)
                throw new InvalidOperationException("The object has been initialized.");
            if (dto.Id == Guid.Empty)
                throw new ArgumentException("GUID value is empty.", nameof(dto.Id));
            if (dto.Modified < dto.Created)
                throw new ArgumentException("The modified date less than the created date.", nameof(dto.Modified));

            id = dto.Id;
            created = dto.Created;
            modified = dto.Modified;
            isInitialized = true;
        }

        protected override void NotifyChanges(string propertyName)
        {
            if (!isInitialized)
                return;

            modified = DateTime.Now;
            base.NotifyChanges(propertyName);
        }
    }
}
