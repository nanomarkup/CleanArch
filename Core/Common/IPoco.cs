using System;

namespace Core
{
    public interface IPoco
    {
        event ChangedPocoEventHandler Changed;
    }

    public class Poco : IPoco
    {
        public event ChangedPocoEventHandler Changed;

        public void NotifyPropertyChanged(string propertyName)
        {
            Changed?.Invoke(this, new ChangedPocoEventArgs(propertyName));
        }
    }

    // Represents the method that will handle the Poco.Changed event raised when a poco object is changed.
    public delegate void ChangedPocoEventHandler(object sender, ChangedPocoEventArgs e);

    public class ChangedPocoEventArgs : EventArgs
    {
        public virtual string PropertyName { get; }

        public ChangedPocoEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
