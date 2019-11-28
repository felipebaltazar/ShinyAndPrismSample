using AsyncAwaitBestPractices;
using Prism.Navigation;
using Shiny.Locations;
using ShinyAndPrismSample.Abstractions;
using System;

namespace ShinyAndPrismSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IObserver<string>
    {
        #region Fields

        private readonly IDisposable _locationObservableDisposer;
        private readonly IGpsManager _gpsManager;
        private string currentLocation;

        #endregion

        #region Proppoerties

        public string CurrentLocation
        {
            get => currentLocation;
            set => SetProperty(ref currentLocation, value);
        }

        #endregion

        #region Constructors

        public MainPageViewModel(INavigationService navigationService,
            IGpsManager gpsManager, ILocationObservable locationObservable)
            : base(navigationService)
        {
            Title = "Main Page";
            CurrentLocation = "No location found..";

            _locationObservableDisposer = locationObservable.Subscribe(this);
            _gpsManager = gpsManager;
        }

        #endregion

        #region IObserver<string>

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            CurrentLocation = error.Message;
        }

        public void OnNext(string value)
        {
            CurrentLocation = value;
        }

        #endregion

        #region Override Methods

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_gpsManager.IsListening)
                _gpsManager.StopListener().SafeFireAndForget();

            _gpsManager.StartListener(BuildRequest()).SafeFireAndForget();

            base.OnNavigatedTo(parameters);
        }

        public override void Destroy()
        {
            _locationObservableDisposer.Dispose();
            base.Destroy();
        }

        #endregion

        #region Private Methods

        private static GpsRequest BuildRequest()
        {
            return new GpsRequest
            {
                UseBackground = true,
                Priority = GpsPriority.Highest,
                Interval = TimeSpan.FromSeconds(5),
                ThrottledInterval = TimeSpan.FromSeconds(3)
            };
        }

        #endregion
    }
}
