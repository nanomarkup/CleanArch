using System;
using System.ComponentModel;

namespace Core.Entities
{
    public class DtoDataEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    } 

    public interface IDataEntity : INotifyPropertyChanged
    {        
        // Identification, read only
        Guid? Id { get; }
        // Created date, read only
        DateTime Created { get; }
        // Modified date, read only
        DateTime Modified { get; }
        // Uses for creation a new entity
        Guid Create();
        // Uses for initializing/loading entity
        void Initialize(DtoDataEntity dataEntity);
        // Lock the notification of PropertyChanged event
        void BeginUpdate();
        // Unlock the notification of PropertyChanged event
        void EndUpdate();
        // Get state of object
        bool IsModified();
    }
}
