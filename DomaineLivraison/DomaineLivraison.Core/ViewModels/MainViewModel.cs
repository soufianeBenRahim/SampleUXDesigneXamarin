using DomaineLivraison.Core.Models.Basket;
using DomaineLivraison.Core.Models.Catalog;

using DomaineLivraison.Core.Services.Catalog;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.Services.User;
using DomaineLivraison.Core.ViewModels.Base;
using DomaineLivraison.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DomaineLivraison.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        IDialogService dialogeService;

        public MainViewModel()
        {
            this.MultipleInitialization = true;
            dialogeService = DependencyService.Get<IDialogService>();
        }

       


   
        public override async Task InitializeAsync (IDictionary<string, string> query)
        {
            IsBusy = false;
        }

        public ICommand LogoutCommand => new Command(async () => await LogoutAsync());
        public ICommand VenteCommande => new Command(async () => await VenteAsync());

        private async Task LogoutAsync()
        {
            IsBusy = true;

            // Logout
            await NavigationService.NavigateToAsync(
                "//Login",
                new Dictionary<string, string> { { "Logout", true.ToString() } });

            IsBusy = false;
        }
        public async Task VenteAsync()
        {
            await dialogeService.ShowAlertAsync("vente commancee", "vente", "ok");
        }
    }
}