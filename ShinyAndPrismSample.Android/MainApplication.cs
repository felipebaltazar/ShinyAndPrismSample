using Android.App;
using Android.Runtime;
using Shiny;
using ShinyAndPrismSample.Services;
using System;

namespace ShinyAndPrismSample.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    public sealed class MainApplication : ShinyAndroidApplication<MyFirstShinyApp>
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }
    }
}