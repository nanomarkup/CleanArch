using Core;
using Core.Entities;
using System;

namespace Entities
{
    public abstract class BaseEntity<IAttrs> : IBaseEntity<IAttrs> 
        where IAttrs : IPoco
    {        
        public IAttrs Attrs { get; private set; }
        public event ChangedEntityEventHandler Changed;
        private int updateRef;

        public virtual void Create(IAttrs attrs)
        {
            Validate(attrs);
            Attrs = attrs;
            Attrs.Changed += AttrsChanged;
            NotifyChanges(nameof(Create));
        }        

        public virtual void Initialize(IAttrs attrs)
        {
            Validate(attrs);
            Attrs = attrs;
            Attrs.Changed += AttrsChanged;
        }

        public virtual void Validate(IAttrs attrs)
        {
            throw new NotImplementedException("Validate method should be overrided in a descendant class.");
        }

        public void BeginUpdate()
        {
            updateRef++;
        }

        public void EndUpdate()
        {            
            if (updateRef > 0)
            {
                updateRef--;
                // Perform Changed event after the updating is finished (updateRef equals to zero)
                if (updateRef == 0)
                    NotifyChanges(nameof(EndUpdate));
            }
        }

        protected virtual void NotifyChanges(string name)
        {
            Changed?.Invoke(this, new ChangedEntityEventArgs(name));
        }

        protected virtual void AttrsChanged(object sender, ChangedPocoEventArgs e)
        {
            NotifyChanges(e.PropertyName);
        }
    }
}
