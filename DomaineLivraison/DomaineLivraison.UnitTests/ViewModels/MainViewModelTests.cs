using DomaineLivraison.Core.Models.Navigation;
using DomaineLivraison.Core.Services.Settings;
using DomaineLivraison.Core.ViewModels;
using DomaineLivraison.Core.ViewModels.Base;
using DomaineLivraison.UnitTests.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace DomaineLivraison.UnitTests
{
    public class MainViewModelTests
    {
        public MainViewModelTests()
        {
            ViewModelLocator.UpdateDependencies(true);
            Xamarin.Forms.DependencyService.RegisterSingleton<ISettingsService>(new MockSettingsService());
        }

  
        [Fact]
        public void IsBusyPropertyIsFalseWhenViewModelInstantiatedTest()
        {
            var mainViewModel = new MainViewModel();
            Assert.False(mainViewModel.IsBusy);
        }

        [Fact]
        public async Task IsBusyPropertyIsTrueAfterViewModelInitializationTest()
        {
            var mainViewModel = new MainViewModel();
            await mainViewModel.InitializeAsync(null);
            Assert.True(mainViewModel.IsBusy);
        }
    }
}
