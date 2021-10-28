
using DomaineLivraison.Core.Services.Catalog;
using DomaineLivraison.Core.Services.Dependency;
using DomaineLivraison.Core.Services.FixUri;
using DomaineLivraison.Core.Services.Identity;
using DomaineLivraison.Core.Services.Location;

using DomaineLivraison.Core.Services.OpenUrl;

using DomaineLivraison.Core.Services.RequestProvider;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.Services.User;
using DomaineLivraison.Services;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace DomaineLivraison.Core.ViewModels.Base
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        static ViewModelLocator()
        {
            // Services - by default, TinyIoC will register interface registrations as singletons.
            var settingsService = new SettingsService ();
            var requestProvider = new RequestProvider ();
            Xamarin.Forms.DependencyService.RegisterSingleton<ISettingsService>(settingsService);
            Xamarin.Forms.DependencyService.RegisterSingleton<INavigationService>(new NavigationService(settingsService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDialogService>(new DialogService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IOpenUrlService>(new OpenUrlService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IRequestProvider>(requestProvider);
            Xamarin.Forms.DependencyService.RegisterSingleton<IIdentityService>(new IdentityService(requestProvider));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDependencyService>(new Services.Dependency.DependencyService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IFixUriService>(new FixUriService(settingsService));
            Xamarin.Forms.DependencyService.RegisterSingleton<ILocationService>(new LocationService(requestProvider));
            Xamarin.Forms.DependencyService.RegisterSingleton<ICatalogService>(new CatalogMockService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IUserService>(new UserMockService());



        }

        public static void UpdateDependencies(bool useMockServices)
        {
            // Change injected dependencies
            if (useMockServices)
            {
                Xamarin.Forms.DependencyService.RegisterSingleton<ICatalogService>(new CatalogMockService());
                Xamarin.Forms.DependencyService.RegisterSingleton<IUserService> (new UserMockService());

                UseMockService = true;
            }
            else
            {
                var requestProvider = Xamarin.Forms.DependencyService.Get<IRequestProvider> ();
                var fixUriService = Xamarin.Forms.DependencyService.Get<IFixUriService> ();
                Xamarin.Forms.DependencyService.RegisterSingleton<ICatalogService> (new CatalogService(requestProvider, fixUriService));
                Xamarin.Forms.DependencyService.RegisterSingleton<IUserService> (new UserService(requestProvider));

                UseMockService = false;
            }
        }

        public static T Resolve<T>() where T : class
        {
            return Xamarin.Forms.DependencyService.Get<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            var viewModel = Activator.CreateInstance (viewModelType);

            view.BindingContext = viewModel;
        }
    }
}