using System.Collections.Generic;
using DomaineLivraison.Core.Models.Basket;
using DomaineLivraison.Core.Models.Catalog;
using DomaineLivraison.Core.Models.Marketing;

namespace DomaineLivraison.Core.Services.FixUri
{
    public interface IFixUriService
    {
        void FixCatalogItemPictureUri(IEnumerable<CatalogItem> catalogItems);
        void FixBasketItemPictureUri(IEnumerable<BasketItem> basketItems);
        void FixCampaignItemPictureUri(IEnumerable<CampaignItem> campaignItems);
    }
}
