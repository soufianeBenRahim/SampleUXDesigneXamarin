using DomaineLivraison.Core.Models.Basket;
using DomaineLivraison.Core.Models.Catalog;

using DomaineLivraison.Core.Services.Catalog;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.Services.User;
using DomaineLivraison.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DomaineLivraison.Core.ViewModels
{
    public class VenteViewModel : ViewModelBase
    {

        private int _badgeCount;

        private ISettingsService _settingsService;
        private IUserService _userService;

        public VenteViewModel()
        {
            this.MultipleInitialization = true;

        
            _settingsService = DependencyService.Get<ISettingsService> ();
            _userService = DependencyService.Get<IUserService> ();
        }

        public int BadgeCount
        {
            get => _badgeCount;
            set
            {
                _badgeCount = value;
                RaisePropertyChanged(() => BadgeCount);
            }
        }

        public ICommand FilterCommand => new Command(async () => await FilterAsync());

		public ICommand ClearFilterCommand => new Command(async () => await ClearFilterAsync());


        public override async Task InitializeAsync (IDictionary<string, string> query)
        {
            IsBusy = true;

            // Get Catalog, Brands and Types
       


            IsBusy = false;
        }


        private async Task FilterAsync()
        {
            try
            {    
                IsBusy = true;

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ClearFilterAsync()
        {
            IsBusy = true;

        

            IsBusy = false;
        }

    }
}