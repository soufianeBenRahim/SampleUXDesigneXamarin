using System;
using System.Collections.Generic;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.ViewModels.Base;
using DomaineLivraison.Core.Views;
using Xamarin.Forms;

namespace DomaineLivraison.Core
{
    public partial class AppShell : Shell
    {
        public AppShell ()
        {
            InitializeRouting ();
            InitializeComponent ();

            var settingsService = ViewModelLocator.Resolve<ISettingsService> ();

            if (string.IsNullOrEmpty (settingsService.AuthAccessToken))
            {
                this.GoToAsync ("//login");
            }
        }

        private void InitializeRouting()
        {
            Routing.RegisterRoute ("Settings", typeof (SettingsView));
        }


    }
}
