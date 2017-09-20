using System;
using Core.Entities;
using System.ComponentModel;

namespace Entities
{   
    public class DataEntity : IDataEntity
    {
        Guid? id;
        DateTime created;
        DateTime modified;        
        int updateRef;
        bool isModified;
        bool isInitialized;

        public Guid? Id { get { return id; } }
        public DateTime Created { get { return created; } }
        public DateTime Modified { get { return modified; } }
        public event PropertyChangedEventHandler PropertyChanged;        

        public Guid Create()
        {
            if (id != null)
                throw new InvalidOperationException("The instance has been already initialized.");

            id = Guid.NewGuid();
            created = DateTime.Now;
            modified = Created;
            isInitialized = true;
            return Id.Value;
        }

        public void Initialize(DtoDataEntity dataEntity)
        {
            if (id != null)
                throw new InvalidOperationException("The instance has been already initialized.");
            if (dataEntity.Id == Guid.Empty)
                throw new ArgumentException("GUID value is empty.", nameof(dataEntity.Id));
            if (dataEntity.Modified < dataEntity.Created)
                throw new ArgumentException("The modified date less than the created date.", nameof(dataEntity.Modified));

            id = dataEntity.Id;
            created = dataEntity.Created;
            modified = dataEntity.Modified;
            isInitialized = true;
        }

        public void BeginUpdate()
        {
            updateRef++;
        }

        public void EndUpdate()
        {
            if (updateRef > 0)
                updateRef--;
            // Perform PropertyChanged event after the updating is finished
            if (updateRef == 0 && IsModified())
                Changed(nameof(EndUpdate));
        }

        public bool IsModified()
        {
            return isModified;
        }

        protected virtual void Changed(string propertyName)
        {
            if (!isInitialized)
                return;

            modified = DateTime.Now;
            if (updateRef > 0)
                isModified = true;
            else
            {
                isModified = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
