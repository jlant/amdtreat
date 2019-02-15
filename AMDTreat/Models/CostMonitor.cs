using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDTreat.Models
{
    /// <summary>
    ///  The provider in the Observer Pattern. Sends notifications to observers.
    /// </summary>
    public class CostMonitor : IObservable<Cost>
    {

        public List<IObserver<Cost>> observers;

        /// <summary>
        /// Called by observers that wish to receive notifications from the provider.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<Cost> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            //observer.OnNext();

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Cost>> _observers;
            private IObserver<Cost> _observer;

            public Unsubscriber(List<IObserver<Cost>> observers, IObserver<Cost> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }

        public CostMonitor()
        {
            observers = new List<IObserver<Cost>>();
        }
    }
}
