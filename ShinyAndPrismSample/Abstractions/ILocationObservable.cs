using Shiny.Locations;
using System;

namespace ShinyAndPrismSample.Abstractions
{
    public interface ILocationObservable : IObservable<string>
    {
        void OnLocationChanged(IGpsReading reading);
    }
}
