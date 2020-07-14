using Shiny.Locations;
using System.Threading.Tasks;

namespace ShinyAndPrismSample.Services
{
    /// Delegate utilizado para tratar a localizaçao com o app em background
    public sealed class MyShinyLocationService : IGpsDelegate
    {
        public Task OnReading(IGpsReading reading)
        {
            //Execute aqui suas açoes em background
            return Task.CompletedTask;
        }
    }
}
