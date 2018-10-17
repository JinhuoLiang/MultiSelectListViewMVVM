using System.ComponentModel;

namespace MultiSelectListViewMVVM
{
    public class PropertyChangedObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        protected PropertyChangedObject()
        {
            NotificationEnabled = true;
        }

        /// <summary>
        /// Enable or Disable property changed notifications.
        /// </summary>
        public bool NotificationEnabled { get; set; }

        /// <summary>
        /// Property Changed Event Handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify PropertyChangedEvent with property name
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (!NotificationEnabled || PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Sends notification with specified PropertyChangedEventArgs
        /// </summary>
        /// <param name="args">Property changed event arguments</param>
        protected void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            if (!NotificationEnabled || PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, args);
        }
    }
}