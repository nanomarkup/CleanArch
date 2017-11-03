using System;

namespace Core.Entities
{
    public interface IBaseEntity<IAttrs>
        where IAttrs : IPoco
    {
        // Entity attributes
        IAttrs Attrs { get; }
        // Occurs when an entity is changed.
        event ChangedEntityEventHandler Changed;
        // Create a new entity
        void Create(IAttrs attrs);
        // Initialize/load the entity
        void Initialize(IAttrs attrs);
        // Validate attributes
        void Validate(IAttrs attrs);
        // Lock the handling of Changed event
        void BeginUpdate();
        // Unlock the handling of Changed event
        void EndUpdate();         
    }    

    // Represents the method that will handle the IBaseEntity.Changed event raised when an entity is changed.
    public delegate void ChangedEntityEventHandler(object sender, ChangedEntityEventArgs e);

    public class ChangedEntityEventArgs : EventArgs
    {
        public virtual string AttributeName { get; }

        public ChangedEntityEventArgs(string attributeName)
        {
            AttributeName = attributeName;
        }
    }
}
