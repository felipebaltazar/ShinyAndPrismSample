using AsyncAwaitBestPractices;
using Prism.Navigation;
using Shiny.Locations;
using System;

namespace ShinyAndPrismSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly IGpsManager _gpsManager;
        private readonly IDisposable _gpsDisposer;
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

        public MainPageViewModel(INavigationService navigationService, IGpsManager gpsManager)
            : base(navigationService)
        {
            Title = "Main Page";
            CurrentLocation = "No location found..";

            _gpsManager = gpsManager;
            _gpsDisposer =_gpsManager.WhenReading()
                .Subscribe(OnNext, OnError);
        }

        #endregion

        #region IObserver<string>

        public void OnError(Exception error)
        {
            CurrentLocation = error.Message;
        }

        public void OnNext(IGpsReading reading)
        {
            CurrentLocation = $"Latitude: {reading.Position.Latitude} | Longitude: {reading.Position.Longitude}";
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
            base.Destroy();
            _gpsDisposer?.Dispose();
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
