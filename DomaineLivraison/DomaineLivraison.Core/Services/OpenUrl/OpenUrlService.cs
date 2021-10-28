using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DomaineLivraison.Core.Services.OpenUrl
{
    public class OpenUrlService : IOpenUrlService
    {
        public async Task OpenUrl(string url)
        {
            if (await Launcher.CanOpenAsync(url))
            {
                await Launcher.OpenAsync (url);
            }
        }
    }
}
