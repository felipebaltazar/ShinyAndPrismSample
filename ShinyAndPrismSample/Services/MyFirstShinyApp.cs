using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Shiny;
using Shiny.Prism;
using ShinyAndPrismSample.Abstractions;

namespace ShinyAndPrismSample.Services
{
    public sealed class MyFirstShinyApp : PrismStartup
    {
        public MyFirstShinyApp() : base(PrismContainerExtension.Current)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILocationObservable, LocationObservable>();
            services.UseGps<MyShinyLocationService>();
        }
    }
}
