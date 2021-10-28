using DomaineLivraison.Core.ViewModels;
using DomaineLivraison.Core.ViewModels.Base;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DomaineLivraison.Core.Views
{
    public partial class MainView : ContentPageBase
    {


        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

        }

        private void OnFilterChanged(object sender, EventArgs e)
        {

        }
    }
}