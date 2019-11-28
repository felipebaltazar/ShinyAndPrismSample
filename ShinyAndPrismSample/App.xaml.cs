using System;
using System.Diagnostics;
using AsyncAwaitBestPractices;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using ShinyAndPrismSample.ViewModels;
using ShinyAndPrismSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ShinyAndPrismSample
{
    public partial class App : PrismApplication
    {
        protected override IContainerExtension CreateContainerExtension() => PrismContainerExtension.Current;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SafeFireAndForgetExtensions.SetDefaultExceptionHandling(LogException);
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        private void LogException(Exception obj)
        {
            Debug.WriteLine(obj.Message);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
