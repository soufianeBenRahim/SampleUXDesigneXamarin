using DomaineLivraison.Core;
using DomaineLivraison.Core.Services.Location;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.Services.Theme;
using DomaineLivraison.Core.ViewModels.Base;
using DomaineLivraison.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DomaineLivraison
{
    public partial class App : Application
    {
        ISettingsService _settingsService;

        public App()
        {
            InitializeComponent();

            InitApp();

            MainPage = new AppShell ();
        }

        private void InitApp()
        {
            _settingsService = ViewModelLocator.Resolve<ISettingsService>();
            if (!_settingsService.UseMocks)
                ViewModelLocator.UpdateDependencies(_settingsService.UseMocks);
        }


        protected override async void OnStart()
        {
            base.OnStart();

            if (_settingsService.AllowGpsLocation && !_settingsService.UseFakeLocation)
            {
                await GetGpsLocation();
            }
            if (!_settingsService.UseMocks && !string.IsNullOrEmpty(_settingsService.AuthAccessToken))
            {
                await SendCurrentLocation();
            }

            OnResume();
        }

        protected override void OnSleep()
        {
            SetStatusBar();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            SetStatusBar();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetStatusBar();
            });
        }

        void SetStatusBar()
        {
            var nav = Current.MainPage as NavigationPage;

            var e = DependencyService.Get<ITheme>();
            if (Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.Black, false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.Black;
                    nav.BarTextColor = Color.White;
                }
            }
            else
            {
                e?.SetStatusBarColor(Color.White, true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.White;
                    nav.BarTextColor = Color.Black;
                }
            }
        }

        private async Task GetGpsLocation()
        {
            try
            {
                var request = new GeolocationRequest (GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync (request, CancellationToken.None).ConfigureAwait(false);

                if (location != null)
                {
                    _settingsService.Latitude = location.Latitude.ToString ();
                    _settingsService.Longitude = location.Longitude.ToString ();
                }
            }
            catch (Exception ex)
            {
                if (ex is FeatureNotEnabledException || ex is FeatureNotEnabledException || ex is PermissionException)
                {
                    _settingsService.AllowGpsLocation = false;
                }

                // Unable to get location
                Debug.WriteLine(ex);
            }
        }

        private async Task SendCurrentLocation()
        {
            var location = new Core.Models.Location.Location
            {
                Latitude = double.Parse(_settingsService.Latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(_settingsService.Longitude, CultureInfo.InvariantCulture)
            };

            var locationService = ViewModelLocator.Resolve<ILocationService>();
            await locationService.UpdateUserLocation(location, _settingsService.AuthAccessToken);
        }
    }
}