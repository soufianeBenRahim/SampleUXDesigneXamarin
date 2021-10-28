using System.Threading.Tasks;

namespace DomaineLivraison.Core.Services.Location
{    
    public interface ILocationService
    {
        Task UpdateUserLocation(DomaineLivraison.Core.Models.Location.Location newLocReq, string token);
    }
}