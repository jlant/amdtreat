using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        #region Utility methods

        /// <summary>
        /// Notify of a property change and optional additional dependencies.
        /// </summary>
        public void Notify([CallerMemberName] string propertyName = null, params string[] additionalNames)
        {
            OnPropertyChanged(propertyName);
            foreach (var name in additionalNames)
            {
                OnPropertyChanged(name);
            }
        }

        /// <summary>
        /// Makes a change to the supplied reference if different.
        /// If different, notify of a property change and optional additional dependencies.
        /// <example>
        /// public int PropertyName
        /// {
        ///     get { return _propertyName; }
        ///     set { ChangeAndNotify(ref _propertyName, value);}
        /// }
        /// 
        /// public int PropertyName
        /// {
        ///     get { return _propertyName; }
        ///     set { ChangeAndNotify(ref _propertyName, value, "PropertyName", "AdditionalRelatedPropertyName"); }
        /// }
        /// </example>
        /// </summary>
        public bool ChangeAndNotify<T>(ref T toChange, T newValue, [CallerMemberName] string propertyName = null, params string[] additionalNames)
        {
            var cmp = EqualityComparer<T>.Default;
            if (cmp.Equals(toChange, newValue) == false)
            {
                toChange = newValue;
                OnPropertyChanged(propertyName);
                foreach (var name in additionalNames)
                {
                    OnPropertyChanged(name);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Makes a change to the supplied reference if different.
        /// If different, notify of a property change and optional additional dependencies then call action.
        /// <example>
        /// public int PropertyName
        /// {
        ///     get { return _propertyName; }
        ///     set { ChangeAndNotify(ref _propertyName, value, () => SomeActionOnSuccess()); }
        /// }
        /// </example>
        /// </summary>
        public bool ChangeAndNotifyWithAction<T>(ref T toChange, T newValue, Action action, [CallerMemberName] string propertyName = null, params string[] additionalNames)
        {
            var doAction = ChangeAndNotify(ref toChange, newValue, propertyName, additionalNames);
            if (doAction)
            {
                action();
            }
            return doAction;
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
