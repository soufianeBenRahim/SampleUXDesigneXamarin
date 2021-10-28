using System.Threading.Tasks;

namespace DomaineLivraison.Core.Services.OpenUrl
{
    public interface IOpenUrlService
    {
        Task OpenUrl(string url);
    }
}
