using DomaineLivraison.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomaineLivraison.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync (string route, IDictionary<string, string> routeParameters = null);
    }
}