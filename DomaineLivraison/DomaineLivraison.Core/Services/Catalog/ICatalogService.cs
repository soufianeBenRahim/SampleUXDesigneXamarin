using DomaineLivraison.Core.Models.Catalog;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DomaineLivraison.Core.Services.Catalog
{
    public interface ICatalogService
    {
        Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync();
        Task<ObservableCollection<CatalogItem>> FilterAsync(int catalogBrandId, int catalogTypeId);
        Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync();
        Task<ObservableCollection<CatalogItem>> GetCatalogAsync();
    }
}
