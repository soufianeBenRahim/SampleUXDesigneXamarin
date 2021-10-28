﻿using DomaineLivraison.Core.Extensions;
using DomaineLivraison.Core.Models.Orders;
using DomaineLivraison.Core.Models.User;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DomaineLivraison.Core.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private ObservableCollection<Order> _orders;
        private Order _selectedOrder;

        public ProfileViewModel()
        {
            this.MultipleInitialization = true;

            _settingsService = DependencyService.Get<ISettingsService> ();
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                RaisePropertyChanged(() => Orders);
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (value == null)
                    return;
                _selectedOrder = null;
                RaisePropertyChanged(() => SelectedOrder);
            }
        }

        public ICommand LogoutCommand => new Command(async () => await LogoutAsync());

        public ICommand OrderDetailCommand => new Command<Order>(async (order) => await OrderDetailAsync(order));

        public override async Task InitializeAsync (IDictionary<string, string> query)
        {
            IsBusy = true;

            // Get orders
            var authToken = _settingsService.AuthAccessToken;

            IsBusy = false;
        }

        private async Task LogoutAsync()
        {
            IsBusy = true;

            // Logout
            await NavigationService.NavigateToAsync(
                "//Login",
                new Dictionary<string, string> { { "Logout", true.ToString () } });

            IsBusy = false;
        }

        private async Task OrderDetailAsync(Order order)
        {
            await NavigationService.NavigateToAsync(
                "OrderDetail",
                new Dictionary<string, string> { { nameof (Order.OrderNumber), order.OrderNumber.ToString() } });
        }
    }
}