using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities
{
    public class BaseEntity : IBaseEntity
    {
        int updateRef;
        bool isModified;
        protected bool isInitialized;

        public event EntityChangedEventHandler Changed;

        public void BeginUpdate()
        {
            updateRef++;
        }

        public void EndUpdate()
        {
            if (updateRef > 0)
                updateRef--;

            // Perform Changed event after the updating is finished (updateRef equals to zero)
            if (updateRef == 0 && IsModified())
                NotifyChanges(nameof(EndUpdate));
        }

        public bool IsModified()
        {
            return isModified;
        }

        protected virtual void NotifyChanges(string propertyName)
        {
            if (!isInitialized)
                return;
            else if (updateRef > 0)
                isModified = true;
            else
            {
                isModified = false;
                Changed?.Invoke(this, new EntityChangedEventArgs(propertyName));
            }
        }
    }
}
