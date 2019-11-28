using Shiny.Locations;
using ShinyAndPrismSample.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShinyAndPrismSample.Services
{
    public sealed class MyShinyLocationService : IGpsDelegate
    {
        private readonly ILocationObservable _locationObserver;

        public MyShinyLocationService(ILocationObservable locationObserver)
        {
            _locationObserver = locationObserver;
        }

        public Task OnReading(IGpsReading reading)
        {
            if (reading?.Position != null)
                _locationObserver.OnLocationChanged(reading);

            return Task.CompletedTask;
        }
    }

    public sealed class LocationObservable : ILocationObservable
    {
        private readonly IList<IObserver<string>> _observers = new List<IObserver<string>>();

        public void OnLocationChanged(IGpsReading reading)
        {
            Parallel.ForEach(_observers, (o) => o.OnNext($"Latitude: {reading.Position.Latitude} | Longitude: {reading.Position.Longitude}"));
        }

        public IDisposable Subscribe(IObserver<string> newObserver)
        {
            if(!_observers.Contains(newObserver))
                _observers.Add(newObserver);

            return new ObserverDisposer(_observers, newObserver);
        }
    }

    public sealed class ObserverDisposer : IDisposable
    {
        private readonly IObserver<string> _observer;
        private readonly IList<IObserver<string>> _observersPersitent;

        public ObserverDisposer(IList<IObserver<string>> observersPersitent,
            IObserver<string> observer)
        {
            _observer = observer;
            _observersPersitent = observersPersitent;
        }

        public void Dispose()
        {
            if (_observersPersitent.Contains(_observer))
                _observersPersitent.Remove(_observer);

            GC.SuppressFinalize(this);
        }
    }
}
