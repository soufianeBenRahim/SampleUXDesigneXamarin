using System.Collections.Generic;
using DomaineLivraison.Core.Models.Catalog;

namespace DomaineLivraison.Core.Services.FixUri
{
    public interface IFixUriService
    {
        void FixCatalogItemPictureUri(IEnumerable<CatalogItem> catalogItems);
    }
}
