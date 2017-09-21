using System;

namespace Core.Entities
{
    public interface IBaseEntity
    {
        // Occurs when an entity is changed.
        event EntityChangedEventHandler Changed;
        // Lock the handling of Changed event
        void BeginUpdate();
        // Unlock the handling of Changed event
        void EndUpdate();
        // Is entity modified
        bool IsModified();        
    }    

    // Represents the method that will handle the IBaseEntity.Changed event raised when an entity is changed.
    public delegate void EntityChangedEventHandler(object sender, EntityChangedEventArgs e);

    public class EntityChangedEventArgs : EventArgs
    {
        public virtual string PropertyName { get; }

        public EntityChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
