using DomaineLivraison.Core.ViewModels;
using DomaineLivraison.Core.ViewModels.Base;
using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DomaineLivraison.Core.Views
{
    public partial class UtilisateurView : ContentPageBase
    {
        private FiltersView _filterView = new FiltersView();

        public UtilisateurView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _filterView.BindingContext = BindingContext;
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            Navigation.ShowPopup (_filterView);
        }
    }
}