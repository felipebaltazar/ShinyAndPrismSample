using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Shiny;
using Shiny.Prism;

namespace ShinyAndPrismSample.Services
{
    public sealed class MyFirstShinyApp : PrismStartup
    {
        public MyFirstShinyApp() : base(PrismContainerExtension.Current)
        {
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.UseGps<MyShinyLocationService>();
        }
    }
}
